using AI.Criteria;
using System;

namespace AI_Test
{
    public class ConsoleCriteria : ICriteria
    {
        public bool IsTrue()
        {
            Console.WriteLine("Checking criteria... enter \"Yes\" to return True ");
            return Console.ReadLine().Equals("Yes");
        }
    }
}
