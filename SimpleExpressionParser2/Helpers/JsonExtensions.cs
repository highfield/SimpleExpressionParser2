using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Core
{
    public static class JsonExtensions
    {

        public static object? GetBoxed(
            this JToken jnode
            )
        {
            switch (jnode.Type)
            {
                case JTokenType.Boolean: return (bool)jnode;
                case JTokenType.Float: return (double)jnode;
                case JTokenType.Integer:
                    var l = (long)jnode;
                    return (l >= int.MinValue && l <= int.MaxValue) ? (int)l : l;
                case JTokenType.Null: return null;
                case JTokenType.String: return (string?)jnode;
                case JTokenType.Date: return (DateTime)jnode;
                default: throw new NotSupportedException();
            }
        }

    }
}
