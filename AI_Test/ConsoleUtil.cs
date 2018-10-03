using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Test
{
    /// <summary>
    /// Class that contains helper methods to make debugging using the console easier
    /// </summary>
    internal static class ConsoleUtil
    {

        /// <summary>
        /// Writes a line of console output in a certain color
        /// </summary>
        /// <param name="msg"> The message </param>
        /// <param name="color"> The color of the text </param>
        public static void WriteLine(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(msg);

            Console.ResetColor();
        }
    }
}
