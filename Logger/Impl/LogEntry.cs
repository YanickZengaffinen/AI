using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public class LogEntry<T> : ILogEntry<T> where T : ILoggable
    {
        public Flag Flag { get; }

        public DateTime TimeStamp { get; }

        public T Action { get; }

        public LogEntry(T action, Flag flag, DateTime timestamp)
        {
            this.Flag = flag;
            this.Action = action;
            this.TimeStamp = timestamp;
        }

        public string ToLogString()
        {
            return $"[{Enum.GetName(typeof(Flag), Flag)}] [{TimeStamp.ToString("yyyyMMddHHmmssfff")}] {Action.ToLogString()}";
        }
    }
}
