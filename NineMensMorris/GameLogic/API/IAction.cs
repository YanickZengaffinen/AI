using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// An action a player makes in a game
    /// </summary>
    public interface IAction : ILoggable
    {
        /// <summary>
        /// The player that executes the action
        /// </summary>
        IPlayer Player { get; }
    }
}
