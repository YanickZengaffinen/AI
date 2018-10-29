using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    /// <summary>
    /// Represents a simple log text message
    /// </summary>
    public class MessageLoggable : ILoggable
    {
        private string message;

        public MessageLoggable(string message)
        {
            this.message = message;
        }

        public string ToLogString()
        {
            return message;
        }
    }
}
