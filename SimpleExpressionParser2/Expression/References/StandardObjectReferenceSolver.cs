using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet.Core.Expression
{
    public class StandardObjectReferenceSolver
        : XReferenceSolverBase<object>
    {

        public override XSolverResult GetValue(XTokenRefId token)
        {
            object? value = null;

            if (token.Data is string path)
            {
                ReflectionHelpers.TryGetValue(this.Context, path, out value);
            }

            return XSolverResult.FromData(value);
        }

    }
}
