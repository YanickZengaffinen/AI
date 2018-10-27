using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic.Util
{
    /// <summary>
    /// Represents a value-tuple of an object and a rating.
    /// Could be replaced by a tuple value if readability doesn't matter too much.
    /// </summary>
    public class RatedObject<T>
    {
        /// <summary>
        /// The rated object
        /// </summary>
        public T Object { get; }

        /// <summary>
        /// The rating of the object
        /// </summary>
        public double Rating { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public RatedObject(T @object, double rating)
        {
            Object = @object;
            Rating = rating;
        }
    }
}
