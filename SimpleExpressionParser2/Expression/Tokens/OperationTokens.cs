using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/**
 * Tabella delle priorità 
 * Vedi: https://www.tutorialspoint.com/csharp/csharp_operators_precedence.htm
 * 
 *  Prio    Category            Operator
 *  --------------------------------------------------------
 *  15      Postfix             () [] -> . ++ --
 *  14      Unary               + - ! ~ ++ - - (type)* & sizeof
 *  13      Multiplicative      * / %
 *  12      Additive            + -
 *  11      Shift               << >>
 *  10      Relational          < <= > >=
 *  9       Equality            == !=
 *  8       Bitwise AND         &
 *  7       Bitwise XOR         ^
 *  6       Bitwise OR          |
 *  5       Logical AND         &&
 *  4       Logical OR          ||
 *  3       Conditional         ?:
 *  2       Assignment          = += -= *= /= %= >>= <<= &= ^= |=
 *  1       Comma               ,
 **/
namespace Cet.Core.Expression
{
    public interface ITokenAssociative { }


    public sealed class XTokenOperAdd : XToken, ITokenAssociative
    {
        public XTokenOperAdd() : base(2, 12, null) { }


        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            double value = XSolverHelpers.AsDouble(result.Data);

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value += XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperSub : XToken, ITokenAssociative
    {
        public XTokenOperSub() : base(2, 12, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            double value = XSolverHelpers.AsDouble(result.Data);

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value -= XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperMul : XToken, ITokenAssociative
    {
        public XTokenOperMul() : base(2, 13, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            double value = XSolverHelpers.AsDouble(result.Data);

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value *= XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperDiv : XToken, ITokenAssociative
    {
        public XTokenOperDiv() : base(2, 13, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            double value = XSolverHelpers.AsDouble(result.Data);

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value /= XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperMod : XToken, ITokenAssociative
    {
        public XTokenOperMod() : base(2, 13, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            double value = XSolverHelpers.AsDouble(result.Data);

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value = value % XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperLogicalOr : XToken, ITokenAssociative
    {
        public XTokenOperLogicalOr() : base(2, 4, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            bool value = XSolverHelpers.AsBool(result.Data);
            if (value)
            {
                return XSolverResult.FromData(value);
            }

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value = XSolverHelpers.AsBool(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperLogicalAnd : XToken, ITokenAssociative
    {
        public XTokenOperLogicalAnd() : base(2, 5, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            bool value = XSolverHelpers.AsBool(result.Data);
            if (value == false)
            {
                return XSolverResult.FromData(value);
            }

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value = XSolverHelpers.AsBool(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperBitwiseNot : XToken, ITokenAssociative
    {
        public XTokenOperBitwiseNot() : base(1, 14, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            var value = ~(int)XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperBitwiseOr : XToken, ITokenAssociative
    {
        public XTokenOperBitwiseOr() : base(2, 6, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            var value = (int)XSolverHelpers.AsDouble(result.Data);

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value = value | (int)XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperBitwiseAnd : XToken, ITokenAssociative
    {
        public XTokenOperBitwiseAnd() : base(2, 8, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            var value = (int)XSolverHelpers.AsDouble(result.Data);

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value = value & (int)XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperBitwiseXor : XToken, ITokenAssociative
    {
        public XTokenOperBitwiseXor() : base(2, 7, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            var value = (int)XSolverHelpers.AsDouble(result.Data);

            result = nb.Resolve(context);
            if (result.Error != null) return result;
            value = value ^ (int)XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperEqual : XToken
    {
        public XTokenOperEqual() : base(2, 9, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult sra = na.Resolve(context);
            if (sra.Error != null) return sra;

            XSolverResult srb = nb.Resolve(context);
            if (srb.Error != null) return srb;

            bool value = XSolverHelpers.Match(sra.Data, srb.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperNotEqual : XToken
    {
        public XTokenOperNotEqual() : base(2, 9, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult sra = na.Resolve(context);
            if (sra.Error != null) return sra;

            XSolverResult srb = nb.Resolve(context);
            if (srb.Error != null) return srb;

            bool value = XSolverHelpers.Match(sra.Data, srb.Data);
            return XSolverResult.FromData(value == false);
        }
    }


    public sealed class XTokenOperLessThan : XToken
    {
        public XTokenOperLessThan() : base(2, 10, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult sra = na.Resolve(context);
            if (sra.Error != null) return sra;

            XSolverResult srb = nb.Resolve(context);
            if (srb.Error != null) return srb;

            int value = XSolverHelpers.Compare(sra.Data, srb.Data);
            return XSolverResult.FromData(value < 0);
        }
    }


    public sealed class XTokenOperLessOrEqualThan : XToken
    {
        public XTokenOperLessOrEqualThan() : base(2, 10, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult sra = na.Resolve(context);
            if (sra.Error != null) return sra;

            XSolverResult srb = nb.Resolve(context);
            if (srb.Error != null) return srb;

            int value = XSolverHelpers.Compare(sra.Data, srb.Data);
            return XSolverResult.FromData(value <= 0);
        }
    }


    public sealed class XTokenOperGreaterThan : XToken
    {
        public XTokenOperGreaterThan() : base(2, 10, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult sra = na.Resolve(context);
            if (sra.Error != null) return sra;

            XSolverResult srb = nb.Resolve(context);
            if (srb.Error != null) return srb;

            int value = XSolverHelpers.Compare(sra.Data, srb.Data);
            return XSolverResult.FromData(value > 0);
        }
    }


    public sealed class XTokenOperGreaterOrEqualThan : XToken
    {
        public XTokenOperGreaterOrEqualThan() : base(2, 10, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult sra = na.Resolve(context);
            if (sra.Error != null) return sra;

            XSolverResult srb = nb.Resolve(context);
            if (srb.Error != null) return srb;

            int value = XSolverHelpers.Compare(sra.Data, srb.Data);
            return XSolverResult.FromData(value >= 0);
        }
    }


    public sealed class XTokenOperMatch : XToken
    {
        public XTokenOperMatch() : base(2, 9, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));
            if (nb == null) throw new ArgumentNullException(nameof(nb));

            XSolverResult sra = na.Resolve(context);
            if (sra.Error != null) return sra;

            XSolverResult srb = nb.Resolve(context);
            if (srb.Error != null) return srb;

            var s = sra.Data as string;
            if (s == null) return XSolverResult.FromData(false);

            var re = srb.Data as Regex;
            if (re == null) return XSolverResult.FromData(false);

            Match match = re.Match(s);
            return XSolverResult.FromData(match.Success);
        }
    }


    public sealed class XTokenOperUnaryPlus : XToken, ITokenAssociative
    {
        public XTokenOperUnaryPlus() : base(1, 14, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            double value = XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(value);
        }
    }


    public sealed class XTokenOperUnaryMinus : XToken, ITokenAssociative
    {
        public XTokenOperUnaryMinus() : base(1, 14, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            double value = XSolverHelpers.AsDouble(result.Data);
            return XSolverResult.FromData(-value);
        }
    }


    public sealed class XTokenOperLogicalNot : XToken
    {
        public XTokenOperLogicalNot() : base(1, 14, null) { }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            if (na == null) throw new ArgumentNullException(nameof(na));

            XSolverResult result = na.Resolve(context);
            if (result.Error != null) return result;
            bool value = XSolverHelpers.AsBool(result.Data);
            return XSolverResult.FromData(value == false);
        }
    }

}
