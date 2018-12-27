using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceTesting.Pseudo
{
    /// <summary>
    /// Contains methods to do some pseudo performance tests
    /// </summary>
    public static class PseudoPerformanceTester
    {
        //The current id of the single test
        private static long testID = long.MinValue;

        //Stores the time at the beginning of each single test
        private static Dictionary<long, DateTime> startTimes = new Dictionary<long, DateTime>();

        /// <summary>
        /// Invokes an action n times and returns the time needed to complete them
        /// </summary>
        public static TimeSpan Test(Action action, int repeats = 1)
        {
            //warm up the system
            action.Invoke();

            DateTime begin = DateTime.Now;

            for(int i = 0; i < repeats; i++)
            {
                action.Invoke();
            }

            return DateTime.Now - begin;
        }

        /// <summary>
        /// Start a performance test at a specific point in code
        /// </summary>
        public static long From()
        {
            long id = testID++;
            startTimes.Add(id, DateTime.Now);

            return id;
        }

        /// <summary>
        /// Stop the performance test with the given id
        /// </summary>
        public static TimeSpan To(long id, bool delete = true)
        {
            var rVal = DateTime.Now - startTimes[id];

            if(delete)
            {
                startTimes.Remove(id);
            }

            return rVal;
        }
    }
}
