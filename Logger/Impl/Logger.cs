using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logger
{
    /// <summary>
    /// Represents a simple logger
    /// </summary>
    public class Logger<T> : ALogger<T> where T : ILoggable
    {
        protected override ILogEntry<T> GenerateLogEntry(T action, Flag flag)
        {
            return new LogEntry<T>(action, flag, DateTime.Now);
        }
    }
}
