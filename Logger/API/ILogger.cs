using System;
using System.Collections.Generic;

namespace Logger
{
    /// <summary>
    /// Generic logger
    /// </summary>
    public interface ILogger<T> where T : ILoggable
    {
        /// <summary>
        /// A list of all the log entries
        /// </summary>
        LinkedList<ILogEntry<T>> Log { get; }

        /// <summary>
        /// Event called whenever a log is being added
        /// </summary>
        event EventHandler<ILogEntry<T>> onLogAdded;

        /// <summary>
        /// Adds a log entry to this logger
        /// </summary>
        void AddLog(T action, Flag flag = Flag.Normal);

        /// <summary>
        /// Clears the log
        /// </summary>
        void Clear();

        /// <summary>
        /// Filter the log entries by their flag
        /// </summary>
        IEnumerable<ILogEntry<T>> Filter(params Flag[] flags);

    }
}
