using System;
using System.Collections.Generic;
using System.Text;

namespace Cet.Core.Expression
{
    partial class Scanner
    {

        private static XToken ScanISO8601(
            Reader reader,
            char head
            )
        {
            reader.MoveNext();
            if (reader.TryPeek(out char prefix))
            {
                int ixold = reader.Index;
                char ch;
                while (reader.TryPeek(out ch) && ch != head) reader.MoveNext();
                if (ch != head)
                {
                    throw new XParserException("Unexpected end of ISO8601 sequence.");
                }
                var body = new string(reader.Source, ixold, reader.Index - ixold);
                reader.MoveNext();
                
                if (prefix == 'P')
                {
                    return new XTokenTimeSpan(
                        ISO8601.StringToTimeSpan(body)
                        );
                }
                else if (char.IsDigit(prefix))
                {
                    return new XTokenDateTime(
                        ISO8601.StringToDateTime(body)
                        );
                }
                else
                {
                    throw new XParserException($"Unsupported ISO8601 format: {body}");
                }
            }

            throw new XParserException("Unexpected end of ISO8601 sequence.");
        }

    }
}
