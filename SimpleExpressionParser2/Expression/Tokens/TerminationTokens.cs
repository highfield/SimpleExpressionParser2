using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cet.Core.Expression
{
    public interface ITokenTermination { }


    public sealed class XTokenNumber : XToken, ITokenTermination
    {
        public XTokenNumber(double value) : base(value) { }

        public override XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc)
        {
            return XSolverResult.FromData(this.Data);
        }
    }


    public sealed class XTokenString : XToken, ITokenTermination
    {
        public XTokenString(string value) : base(value) { }

        public override XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc)
        {
            return XSolverResult.FromData(this.Data);
        }
    }


    public sealed class XTokenMatchParam : XToken, ITokenTermination
    {
        public XTokenMatchParam(string value, string flags) : base(value)
        {
            this.Flags = flags ?? string.Empty;
        }

        public string Flags { get; }

        public override XSolverResult Resolve(
            IXSolverContext context, 
            XTreeNodeBase? na, 
            XTreeNodeBase? nb, 
            XTreeNodeBase? nc
            )
        {
            var pattern = this.Data as string;
            if (pattern == null) return XSolverResult.FromData(null);

            RegexOptions options = RegexOptions.None;
            if (this.Flags.Contains('i')) options |= RegexOptions.IgnoreCase;

            var re = new Regex(pattern, options);
            return XSolverResult.FromData(re);
        }
    }


    public sealed class XTokenBoolean : XToken, ITokenTermination
    {
        public XTokenBoolean(bool value) : base(value) { }

        public override XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc)
        {
            return XSolverResult.FromData(this.Data);
        }
    }


    public sealed class XTokenTimeSpan : XToken, ITokenTermination
    {
        public XTokenTimeSpan(TimeSpan value) : base(value) { }

        public override XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc)
        {
            return XSolverResult.FromData(this.Data);
        }
    }


    public sealed class XTokenDateTime : XToken, ITokenTermination
    {
        public XTokenDateTime(DateTime value) : base(value) { }

        public override XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc)
        {
            return XSolverResult.FromData(this.Data);
        }
    }


    public sealed class XTokenDateTimeOffset : XToken, ITokenTermination
    {
        public XTokenDateTimeOffset(DateTimeOffset value) : base(value) { }

        public override XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc)
        {
            return XSolverResult.FromData(this.Data);
        }
    }


    public sealed class XTokenNull : XToken, ITokenTermination
    {

        public override XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc)
        {
            return XSolverResult.FromData(this.Data);
        }
    }


    public sealed class XTokenRefId : XToken, ITokenTermination
    {
        public XTokenRefId(string value) : base(value) { }

        public override XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc)
        {
            if (context.ReferenceSolver == null) return XSolverResult.FromData(null);
            return context.ReferenceSolver.GetValue(this);
        }
    }
}
