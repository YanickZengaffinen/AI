using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Util.RandomNumberGenerators
{
    /// <summary>
    /// Generates a random number ranging from -1 to +1
    /// </summary>
    public class StandardBiRandom : IRandom
    {
        protected Random random = new Random();

        public double Generate()
        {
            return (random.NextDouble() - .5d) * 2.0d;
        }
    }
}
