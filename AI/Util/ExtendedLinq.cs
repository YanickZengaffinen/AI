using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.Util
{
    /// <summary>
    /// Class that adds more linq like extension methods
    /// </summary>
    public static class ExtendedLinq
    {
        /// <summary>
        /// Selects n random elements from the source
        /// </summary>
        /// <param name="count"> The amount of elements to select </param>
        public static IEnumerable<T> SelectRandomUnique<T>(this IEnumerable<T> enumeration, int count)
        {
            return enumeration.OrderBy(x => Guid.NewGuid()).Take(count);    //TODO: isn't there a more efficient way to solve this problem rather than to order an entire list?
        }

    }
}
