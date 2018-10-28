using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Representing a killing of a man on the board
    /// </summary>
    public class Kill : IAction
    {
        public IPlayer Player { get; }

        /// <summary>
        /// The man at which position should be killed
        /// </summary>
        public Position Target { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public Kill(IPlayer player, Position target)
        {
            Player = player;
            Target = target;
        }

        public string ToLogString()
        {
            return $"Player {Player.ID} killed a man at {Target.ToString()}";
        }
    }
}
