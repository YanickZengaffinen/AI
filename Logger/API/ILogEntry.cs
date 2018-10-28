using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    /// <summary>
    /// A log entry
    /// </summary>
    public interface ILogEntry<T> where T : ILoggable
    {
        /// <summary>
        /// The flag of this entry
        /// </summary>
        Flag Flag { get; }
        
        /// <summary>
        /// When was the entry made
        /// </summary>
        DateTime TimeStamp { get; }

        /// <summary>
        /// What did happen
        /// </summary>
        T Action { get; }

        /// <summary>
        /// Converts the log entry into a string that can be written into a text file
        /// </summary>
        string ToLogString();
    }
}
