using System;
using System.Collections.Generic;
using System.Text;

namespace Cet.Core.Expression
{
    public interface IXTreeSerializer<TOut>
    {
        TOut? Serialize(XTreeNodeBase xtree, object? context);
    }
}
