using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cet.Core.Expression
{
    public class XTreeXmlSerializer 
        : IXTreeSerializer<XElement>
    {

        //TODO use the visitor pattern rather a fixed procedure
        public XElement? Serialize(XTreeNodeBase xtree, object? context)
        {
            var xelem = new XElement(
                xtree.Token.GetType().Name,
                xtree.GetChildren().Select(_ => this.Serialize(_, context))
                );

            if (xtree.Token.Data != null)
            {
                xelem.Add(new XAttribute("data", xtree.Token.Data));
            }

            var mp = xtree.Token as XTokenMatchParam;
            if (mp != null)
            {
                xelem.Add(new XAttribute("flags", mp.Flags));
            }
            return xelem;
        }

    }
}
