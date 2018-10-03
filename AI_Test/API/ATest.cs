using System;

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
            ConsoleUtil.WriteLine("Running test " + name, ConsoleColor.Red);

            Execute();

            Console.WriteLine();
        }

        //The actual test logic
        protected abstract void Execute();

    }
}
