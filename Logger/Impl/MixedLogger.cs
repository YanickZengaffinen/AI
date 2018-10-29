using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    /// <summary>
    /// Logger that can log multiple loggable types and not just one
    /// </summary>
    public class MixedLogger : ALogger<ILoggable>
    {
        /// <summary>
        /// Adds a string message to the log
        /// </summary>
        public void AddLog(string msg, Flag flag = Flag.Normal)
        {
            var logMsg = new MessageLoggable(msg);
            var logEntry = new LogEntry<ILoggable>(logMsg, flag, DateTime.Now);

            Log.AddLast(logEntry);

            //call event
            OnLogAdded(logEntry);
        }

        protected override ILogEntry<ILoggable> GenerateLogEntry(ILoggable action, Flag flag)
        {
            return new LogEntry<ILoggable>(action, flag, DateTime.Now);
        }
    }
}
