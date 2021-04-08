using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet.Core.Expression
{
    public class StandardJsonReferenceSolver
        : XReferenceSolverBase<JToken>
    {

        public override XSolverResult GetValue(XTokenRefId token)
        {
            object? value = null;

            var path = token.Data as string;
            if (path != null)
            {
                JToken? jresult = this.Context?.SelectToken(path);
                value = jresult?.GetBoxed();
            }

            return XSolverResult.FromData(value);
        }

    }
}
