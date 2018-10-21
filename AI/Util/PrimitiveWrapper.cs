using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Util
{
    /// <summary>
    /// Wrapper class for primitive types --> makes them reference types
    /// </summary>
    public class PrimitiveWrapper<T> where T : struct
    {
        /// <summary>
        /// The value behind this wrapper
        /// </summary>
        public T Value { get; set; }
    }
}
