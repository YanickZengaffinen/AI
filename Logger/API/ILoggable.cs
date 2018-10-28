using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public interface ILoggable
    {
        /// <summary>
        /// Converts the loggable into a string
        /// </summary>
        string ToLogString();

    }
}
