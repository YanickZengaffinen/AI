using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Represents the placement of a man on the board
    /// </summary>
    public struct Placement : IAction
    {
        public IPlayer Player { get; }

        /// <summary>
        /// The position the man moved to
        /// </summary>
        public Position Target { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public Placement(IPlayer player, Position target)
        {
            Player = player;
            Target = target;
        }
    }
}
