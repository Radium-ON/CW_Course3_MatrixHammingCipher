using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMatrix.Extensions
{
    public static class IEnumerableExtension
    {
        public static string AppendAll(this IEnumerable<byte> collection, string seperator)
        {
            using (var enumerator = collection.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return string.Empty;
                }

                var builder = new StringBuilder().Append(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    builder.Append(seperator).Append(enumerator.Current);
                }

                return builder.ToString();
            }
        }
    }
}
