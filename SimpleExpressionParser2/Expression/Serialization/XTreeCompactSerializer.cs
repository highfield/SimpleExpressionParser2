using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Core.Expression
{
    public class XTreeCompactSerializer
        : IXTreeSerializer<string>
    {
        public Dictionary<Type, Action<XToken, SerializationContext>> VisitorMap { get; } = new Dictionary<Type, Action<XToken, SerializationContext>>()
        {
            [typeof(XTokenNumber)] = SerializeTokenNumber,
            [typeof(XTokenString)] = SerializeTokenString,
            [typeof(XTokenMatchParam)] = SerializeTokenMatch,
            [typeof(XTokenBoolean)] = SerializeTokenBoolean,
            [typeof(XTokenTimeSpan)] = SerializeTokenTimeSpan,
            [typeof(XTokenDateTime)] = SerializeTokenDateTime,
            [typeof(XTokenDateTimeOffset)] = SerializeTokenDateTimeOffset,
            [typeof(XTokenNull)] = SerializeTokenNull,
            [typeof(XTokenRefId)] = SerializeTokenRefId,

            [typeof(XTokenOperAdd)] = SerializeTokenOperAdd,
            [typeof(XTokenOperSub)] = SerializeTokenOperSub,
            [typeof(XTokenOperMul)] = SerializeTokenOperMul,
            [typeof(XTokenOperDiv)] = SerializeTokenOperDiv,
            [typeof(XTokenOperMod)] = SerializeTokenOperMod,
            [typeof(XTokenOperLogicalOr)] = SerializeTokenOperLogicalOr,
            [typeof(XTokenOperLogicalAnd)] = SerializeTokenOperLogicalAnd,

            [typeof(XTokenOperBitwiseAnd)] = SerializeTokenOperBitwiseAnd,
            [typeof(XTokenOperBitwiseOr)] = SerializeTokenOperBitwiseOr,
            [typeof(XTokenOperBitwiseXor)] = SerializeTokenOperBitwiseXor,
            [typeof(XTokenOperBitwiseNot)] = SerializeTokenOperBitwiseNot,

            [typeof(XTokenOperEqual)] = SerializeTokenOperEqual,
            [typeof(XTokenOperNotEqual)] = SerializeTokenOperNotEqual,
            [typeof(XTokenOperLessThan)] = SerializeTokenOperLessThan,
            [typeof(XTokenOperLessOrEqualThan)] = SerializeTokenOperLessOrEqualThan,
            [typeof(XTokenOperGreaterThan)] = SerializeTokenOperGreaterThan,
            [typeof(XTokenOperGreaterOrEqualThan)] = SerializeTokenOperGreaterOrEqualThan,
            [typeof(XTokenOperMatch)] = SerializeTokenOperMatch,
            [typeof(XTokenOperUnaryPlus)] = SerializeTokenOperUnaryPlus,
            [typeof(XTokenOperUnaryMinus)] = SerializeTokenOperUnaryMinus,
            [typeof(XTokenOperLogicalNot)] = SerializeTokenOperLogicalNot,
        };


        public bool ShouldPad { get; set; }


        public string? Serialize(XTreeNodeBase xtree, object? context)
        {
            var ctx = new SerializationContext();
            ctx.AddSpaces = this.ShouldPad;
            this.Serialize(xtree, ctx, 0);
            return ctx.Builder.ToString();
        }


        private void Serialize(XTreeNodeBase xtree, SerializationContext ctx, int level)
        {
            if (xtree is XTreeNodeTerminal term)
            {
                this.VisitorMap[term.Token.GetType()](term.Token, ctx);
            }
            else if (xtree is XTreeNodeUnary unary)
            {
                this.VisitorMap[unary.Token.GetType()](unary.Token, ctx);
                this.Serialize(unary.Child, ctx, level + 1);
            }
            else if (xtree is XTreeNodeBinary binary)
            {
                if (level != 0) ctx.Builder.Append('(');
                ctx.LastIsAlphaNum = false;

                this.Serialize(binary.LeftChild, ctx, level + 1);
                if (ctx.AddSpaces)
                {
                    ctx.Builder.Append(' ');
                    ctx.LastIsAlphaNum = false;
                }
                this.VisitorMap[binary.Token.GetType()](binary.Token, ctx);
                if (ctx.AddSpaces)
                {
                    ctx.Builder.Append(' ');
                    ctx.LastIsAlphaNum = false;
                }
                this.Serialize(binary.RightChild, ctx, level + 1);

                if (level != 0)
                {
                    ctx.Builder.Append(')');
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }


        private static void SerializeTokenNumber(XToken token, SerializationContext ctx)
        {
            if (ctx.LastIsAlphaNum) ctx.Builder.Append(' ');
            if (token.Data is double value)
            {
                ctx.Builder.Append(value.ToString(CultureInfo.InvariantCulture));
                ctx.LastIsAlphaNum = true;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        private static void SerializeTokenString(XToken token, SerializationContext ctx)
        {
            var value = token.Data as string;
            ctx.Builder.Append('"' + value + '"');
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenMatch(XToken token, SerializationContext ctx)
        {
            var mtk = (XTokenMatchParam)token;
            ctx.Builder.Append($"/{mtk.Data}/{mtk.Flags}");
            ctx.LastIsAlphaNum = true;
        }

        private static void SerializeTokenBoolean(XToken token, SerializationContext ctx)
        {
            if (ctx.LastIsAlphaNum) ctx.Builder.Append(' ');
            if (token.Data is bool value)
            {
                ctx.Builder.Append(value ? "true" : "false");
                ctx.LastIsAlphaNum = true;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        private static void SerializeTokenTimeSpan(XToken token, SerializationContext ctx)
        {
            if (token.Data is TimeSpan value)
            {
                ctx.Builder.Append($"#{ISO8601.TimeSpanToString(value)}#");
                ctx.LastIsAlphaNum = true;
            }
            else
            {
                throw new InvalidCastException();
            }
        }


        private static void SerializeTokenDateTime(XToken token, SerializationContext ctx)
        {
            if (token.Data is DateTime value)
            {
                ctx.Builder.Append($"#{ISO8601.DateTimeToString(value)}#");
                ctx.LastIsAlphaNum = true;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        private static void SerializeTokenDateTimeOffset(XToken token, SerializationContext ctx)
        {
            if (token.Data is DateTimeOffset value)
            {
                ctx.Builder.Append($"#{ISO8601.DateTimeOffsetToString(value)}#");
                ctx.LastIsAlphaNum = true;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        private static void SerializeTokenNull(XToken token, SerializationContext ctx)
        {
            if (ctx.LastIsAlphaNum) ctx.Builder.Append(' ');
            ctx.Builder.Append("null");
            ctx.LastIsAlphaNum = true;
        }

        private static void SerializeTokenRefId(XToken token, SerializationContext ctx)
        {
            if (ctx.LastIsAlphaNum) ctx.Builder.Append(' ');
            ctx.Builder.Append(token.Data);
            ctx.LastIsAlphaNum = true;
        }



        private static void SerializeTokenOperAdd(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("+");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperSub(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("-");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperMul(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("*");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperDiv(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("/");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperMod(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("%");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperLogicalOr(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("||");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperLogicalAnd(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("&&");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperBitwiseAnd(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("&");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperBitwiseOr(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("|");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperBitwiseXor(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("^");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperBitwiseNot(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("~");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperEqual(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("==");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperNotEqual(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("!=");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperLessThan(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("<");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperLessOrEqualThan(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("<=");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperGreaterThan(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append(">");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperGreaterOrEqualThan(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append(">=");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperMatch(XToken token, SerializationContext ctx)
        {
            if (ctx.LastIsAlphaNum) ctx.Builder.Append(' ');
            ctx.Builder.Append("match");
            ctx.LastIsAlphaNum = true;
        }

        private static void SerializeTokenOperUnaryPlus(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("+");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperUnaryMinus(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("-");
            ctx.LastIsAlphaNum = false;
        }

        private static void SerializeTokenOperLogicalNot(XToken token, SerializationContext ctx)
        {
            ctx.Builder.Append("!");
            ctx.LastIsAlphaNum = false;
        }


        public class SerializationContext
        {
            public StringBuilder Builder { get; } = new StringBuilder();
            public bool AddSpaces;

            public bool LastIsAlphaNum;
        }

    }
}
