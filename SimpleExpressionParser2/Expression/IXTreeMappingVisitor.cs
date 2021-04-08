using System;
using System.Collections.Generic;
using System.Text;

namespace Cet.Core.Expression
{
    public interface IXTreeMappingVisitor
    {
        XTreeNodeTerminal MapTerminal(XTreeNodeTerminal node);
        XTreeNodeUnary MapUnary(XTreeNodeUnary node);
        XTreeNodeBinary MapBinary(XTreeNodeBinary node);
    }
}
