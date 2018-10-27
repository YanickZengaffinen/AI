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
        public static IEnumerable<T> SelectRandomUnique<T>(this IEnumerable<T> enumeration, in int count)
        {
            return enumeration.OrderBy(x => Guid.NewGuid()).Take(count);    //TODO: isn't there a more efficient way to solve this problem rather than to order an entire list?
        }

        /// <summary>
        /// Get a row from a 2D array
        /// </summary>
        public static T[] GetRow<T>(this T[,] array, in int row)
        {
            var rowCnt = array.GetLength(0);
            var columnCnt = array.GetLength(1);

            if (row >= rowCnt)
            {
                throw new IndexOutOfRangeException("Row Index Out of Range");
            }

            var rVal = new T[columnCnt];
            for (int i = 0; i < columnCnt; i++)
            {
                rVal[i] = array[row, i];
            }

            return rVal;
        }

        /// <summary>
        /// Get a column from a 2D array
        /// </summary>
        public static T[] GetColumn<T>(this T[,] array, in int column)
        {
            var rowCnt = array.GetLength(0);
            var columnCnt = array.GetLength(1);

            if (column >= columnCnt)
            {
                throw new IndexOutOfRangeException("Column Index Out of Range");
            }

            var rVal = new T[rowCnt];
            for (int i = 0; i < rowCnt; i++)
            {
                rVal[i] = array[i, column];
            }

            return rVal;
        }

    }
}
