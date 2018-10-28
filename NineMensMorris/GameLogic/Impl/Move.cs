using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Represents a movement of a man on the board
    /// </summary>
    public struct Move : IAction
    {
        public IPlayer Player { get; }

        /// <summary>
        /// Where the man is moved from
        /// </summary>
        public Position Start { get; }

        /// <summary>
        /// Where the man is moved to
        /// </summary>
        public Position Destination { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public Move(IPlayer player, Position startPosition, Position target)
        {
            Player = player;
            Start = startPosition;
            Destination = target;
        }

        public string ToLogString()
        {
            return $"Player {Player.ID} moved a man from {Start.ToString()} to {Destination.ToString()}";
        }
    }
}
