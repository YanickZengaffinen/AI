using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Test.API
{
    internal abstract class ATest
    {
        private string name; //the name of this test...

        //C'tor taking in the name of the test
        protected ATest(string name)
        {
            this.name = name;
        }

        //Runs the test and logs some information about it
        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Running test " + name);
            Console.ForegroundColor = ConsoleColor.Gray;

            Execute();

            Console.WriteLine();
        }

        //The actual test logic
        protected abstract void Execute();

    }
}
