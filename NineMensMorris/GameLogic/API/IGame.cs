using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    public interface IGame
    {
        /// <summary>
        /// Representing the structure of a nine men's morris board
        /// </summary>
        Board Board { get; }

        //Events
        /// <summary>
        /// Raised whenever a player places a man
        /// </summary>
        event EventHandler<Placement> onPlaced;

        /// <summary>
        /// Raised whenever a player moves a man
        /// </summary>
        event EventHandler<Move> onMoved;

        /// <summary>
        /// Raised whenever a player kills a man
        /// </summary>
        event EventHandler<Kill> onKilled;

        /// <summary>
        /// Try to place a men on a position for a player
        /// </summary>
        /// <returns> True if valid move </returns>
        bool Place(Placement placement);

        /// <summary>
        /// Try to move a men from a position to 
        /// </summary>
        /// <returns> True if valid move </returns>
        bool Move(Move move);

        /// <summary>
        /// Try to kill a man at a certain position
        /// </summary>
        bool Kill(Kill kill);

    }
}
