using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    public class ManPlacedEventArgs : EventArgs
    {
        /// <summary>
        /// The position where the man has been placed
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// The player who has placed the man
        /// </summary>
        public IPlayer Owner { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public ManPlacedEventArgs(IPlayer owner, Position position)
        {
            Position = position;
            Owner = owner;
        }
    }
}
