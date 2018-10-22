using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Util.RandomNumberGenerators
{
    /// <summary>
    /// Class that uses the default Random class to generate random floatingpoint numbers
    /// </summary>
    public class StandardRandom : IRandom
    {
        private Random random;

        public StandardRandom()
        {
            random = new Random();
        }

        public double Generate()
        {
            return random.NextDouble();
        }

        public double GenerateBi()
        {
            return (random.NextDouble() - .5d) * 2.0d;
        }
    }
}
