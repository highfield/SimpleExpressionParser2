using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cet.Core.Expression
{
    internal static partial class Scanner
    {

        internal static void Scan(Reader reader)
        {
            while (reader.TryPeek(out char ch))
            {
                XToken? token = null;
                switch (ch)
                {
                    case '(':
                        reader.Tokens.Add(new XTokenLParen());
                        reader.MoveNext();
                        break;
                    case ')':
                        reader.Tokens.Add(new XTokenRParen());
                        reader.MoveNext();
                        break;
                    case '&': token = ScanOneOrTwo(reader, ch, new XTokenOperBitwiseAnd(), new XTokenOperLogicalAnd()); break;
                    case '|': token = ScanOneOrTwo(reader, ch, new XTokenOperBitwiseOr(), new XTokenOperLogicalOr()); break;
                    case '!': token = ScanExclamationSign(reader, ch); break;
                    case '=': token = ScanEqualSign(reader, ch); break;
                    case '<': token = ScanLessSign(reader, ch); break;
                    case '>': token = ScanGreaterSign(reader, ch); break;
                    case '"': token = ScanString(reader, ch); break;
                    case '\'': token = ScanString(reader, ch); break;
                    case '/': token = ScanSlash(reader, ch); break;
                    case '_': token = ScanLiteral(reader, ch); break;
                    case '$': token = ScanLiteral(reader, ch); break;
                    case '-': token = ScanMinusSign(reader, ch); break;
                    case '+': token = ScanPlusSign(reader, ch); break;
                    case '*': token = ScanStarSign(reader, ch); break;
                    case '%': token = ScanPercentSign(reader, ch); break;
                    case '^': token = ScanCaretSign(reader, ch); break;
                    case '~': token = ScanTildeSign(reader, ch); break;
                    case '#': token = ScanISO8601(reader, ch); break;
                    default:
                        if (char.IsWhiteSpace(ch))
                        {
                            reader.MoveNext();
                        }
                        else if (char.IsDigit(ch))
                        {
                            token = ScanNumber(reader, ch);
                        }
                        else if (char.IsLetter(ch))
                        {
                            token = ScanLiteral(reader, ch);
                        }
                        else
                        {
                            throw new XParserException($"Illegal character found: {ch}");
                        }
                        break;
                }

                if (token != null)
                {
                    reader.Tokens.Add(token);
                }
            }
        }


        private static XToken ScanNumber(
            Reader reader,
            char head
            )
        {
            int ixold = reader.Index;
            if ("-+".IndexOf(head) >= 0)
            {
                reader.MoveNext();
            }

            int status = 0;
            while (reader.TryPeek(out char ch))
            {
                if (status == 0)
                {
                    //integer part of mantissa (first digit)
                    if (char.IsDigit(ch))
                    {
                        status = 1;
                    }
                    else
                    {
                        throw new XParserException($"Illegal character found: {ch}");
                    }
                }
                else if (status == 1)
                {
                    //integer part of mantissa
                    if (char.IsDigit(ch))
                    {
                        //OK
                    }
                    else if (ch == '.')
                    {
                        status = 10;
                    }
                    else if ("Ee".Contains(ch))
                    {
                        status = 20;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (status == 10)
                {
                    //fractional part of mantissa (first digit)
                    if (char.IsDigit(ch))
                    {
                        status = 11;
                    }
                    else
                    {
                        throw new XParserException($"Illegal character found: {ch}");
                    }
                }
                else if (status == 11)
                {
                    //fractional part of mantissa
                    if (char.IsDigit(ch))
                    {
                        //OK
                    }
                    else if ("Ee".Contains(ch))
                    {
                        status = 20;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (status == 20)
                {
                    //exponent (first digit)
                    if (char.IsDigit(ch))
                    {
                        status = 21;
                    }
                    else if ("-+".Contains(ch))
                    {
                        status = 21;
                    }
                    else
                    {
                        throw new XParserException($"Illegal character found: {ch}");
                    }
                }
                else if (status == 21)
                {
                    //exponent
                    if (char.IsDigit(ch))
                    {
                        //OK
                    }
                    else
                    {
                        break;
                    }
                }
                reader.MoveNext();
            }

            var literal = new string(reader.Source, ixold, reader.Index - ixold);
            return new XTokenNumber(double.Parse(literal, CultureInfo.InvariantCulture));
        }


        private static XToken ScanString(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();
            int ixold = reader.Index;
            char ch;
            while (reader.TryPeek(out ch) && ch != head) reader.MoveNext();
            if (ch != head)
            {
                throw new XParserException("Unexpected end of string.");
            }
            var literal = new string(reader.Source, ixold, reader.Index - ixold);
            reader.MoveNext();

            return new XTokenString(literal);
        }


        private static XToken ScanLiteral(
            Reader reader,
            char head
            )
        {
            bool predicate(char _) => char.IsLetterOrDigit(_) || "_.$".Contains(_);

            int ixold = reader.Index;
            while (reader.TryPeek(out char ch) && predicate(ch)) reader.MoveNext();
            var literal = new string(reader.Source, ixold, reader.Index - ixold);

            switch (literal)
            {
                case "false": return new XTokenBoolean(false);
                case "true": return new XTokenBoolean(true);
                case "null": return new XTokenNull();
                case "match": return new XTokenOperMatch();
                default: return new XTokenRefId(literal);
            }
        }


        private static XToken ScanSlash(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();

            if (reader.Tokens.LastOrDefault() is XTokenOperMatch)
            {
                int ixold = reader.Index;
                char ch;
                while (reader.TryPeek(out ch) && ch != head) reader.MoveNext();
                if (ch != head)
                {
                    throw new XParserException("Unexpected end of pattern.");
                }
                var literal = new string(reader.Source, ixold, reader.Index - ixold);
                reader.MoveNext();

                //flags
                var flags = string.Empty;
                while (reader.TryPeek(out ch) && char.IsWhiteSpace(ch) == false && char.IsLetterOrDigit(ch))
                {
                    if ("i".IndexOf(ch) < 0)
                    {
                        throw new XParserException($"Unsupported Regex match flag: {ch}");
                    }
                    flags += ch;
                    reader.MoveNext();
                }

                return new XTokenMatchParam(literal, flags);
            }

            return new XTokenOperDiv();
        }


        private static XToken ScanOneOrTwo(
            Reader reader,
            char head,
            XToken result1,
            XToken result2
            )
        {
            reader.MoveNext();

            if (reader.TryPeek(out char ch) && ch == head)
            {
                reader.MoveNext();
                return result2;
            }

            return result1;
        }


        private static XToken ScanPlusSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();

            if (reader.Tokens.LastOrDefault() is ITokenTermination ||
                reader.Tokens.LastOrDefault() is XTokenRParen
                )
            {
                return new XTokenOperAdd();
            }
            return new XTokenOperUnaryPlus();
        }


        private static XToken ScanMinusSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();

            if (reader.Tokens.LastOrDefault() is ITokenTermination ||
                reader.Tokens.LastOrDefault() is XTokenRParen
                )
            {
                return new XTokenOperSub();
            }
            return new XTokenOperUnaryMinus();
        }


        private static XToken ScanStarSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();
            return new XTokenOperMul();
        }


        private static XToken ScanPercentSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();
            return new XTokenOperMod();
        }


        private static XToken ScanCaretSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();
            return new XTokenOperBitwiseXor();
        }


        private static XToken ScanTildeSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();
            return new XTokenOperBitwiseNot();
        }


        private static XToken ScanExclamationSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();

            if (reader.TryPeek(out char ch) && ch == '=')
            {
                reader.MoveNext();
                return new XTokenOperNotEqual();
            }
            else
            {
                return new XTokenOperLogicalNot();
            }
        }


        private static XToken ScanEqualSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();

            if (reader.TryPeek(out char ch))
            {
                reader.MoveNext();
                switch (ch)
                {
                    case '=': return new XTokenOperEqual();
                }
            }
            throw new XParserException($"Illegal character found: {ch}");
        }


        private static XToken ScanLessSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();

            if (reader.TryPeek(out char ch) && ch == '=')
            {
                reader.MoveNext();
                return new XTokenOperLessOrEqualThan();
            }
            else
            {
                return new XTokenOperLessThan();
            }
        }


        private static XToken ScanGreaterSign(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();

            if (reader.TryPeek(out char ch) && ch == '=')
            {
                reader.MoveNext();
                return new XTokenOperGreaterOrEqualThan();
            }
            else
            {
                return new XTokenOperGreaterThan();
            }
        }


        private class CharArrayWriter
        {
            private char[] _array = new char[100];
            public char[] Buffer
            {
                get { return this._array; }
            }

            private int _count;
            public int Count
            {
                get { return this._count; }
            }

            public void Add(char ch)
            {
                this._array[this._count] = ch;
                if (++this._count > this._array.Length)
                {
                    Array.Resize(ref this._array, this._array.Length + 100);
                }
            }

            public override string ToString()
            {
                return new string(this._array, 0, this._count);
            }
        }

    }
}
