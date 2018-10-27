using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Custom event args for the movement of a man
    /// </summary>
    public class ManMovedEventArgs : EventArgs
    {
        /// <summary>
        /// Where was the man located before
        /// </summary>
        public Position From { get; }

        /// <summary>
        /// Where is the man located now
        /// </summary>
        public Position To { get; }

        /// <summary>
        /// By whom was the man moved
        /// </summary>
        public IPlayer By { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public ManMovedEventArgs(IPlayer by, Position from, Position to)
        {
            From = from;
            To = to;
            By = by;
        }
    }
}
