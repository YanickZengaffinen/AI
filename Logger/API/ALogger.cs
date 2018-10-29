using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging
{
    /// <summary>
    /// An abstract implementation of a logger with a 
    /// </summary>
    public abstract class ALogger<T> : ILogger<T> where T : ILoggable
    {
        public LinkedList<ILogEntry<T>> Log { get; private set; } = new LinkedList<ILogEntry<T>>();

        public event EventHandler<ILogEntry<T>> onLogAdded;

        public void AddLog(T action, Flag flag = Flag.Normal)
        {
            var newEntry = GenerateLogEntry(action, flag);
            Log.AddLast(newEntry);

            //Call event
            OnLogAdded(newEntry);
        }

        /// <summary>
        /// Method that creates a new log entry
        /// </summary>
        protected abstract ILogEntry<T> GenerateLogEntry(T action, Flag flag);

        /// <summary>
        /// Raises the onLogAdded Event
        /// </summary>
        protected void OnLogAdded(ILogEntry<T> newEntry)
        {
            onLogAdded?.Invoke(this, newEntry);
        }

        public void Clear()
        {
            Log.Clear();
        }

        public IEnumerable<ILogEntry<T>> Filter(params Flag[] flags)
        {
            return Log.Where(x => flags.Contains(x.Flag));
        }
    }
}
