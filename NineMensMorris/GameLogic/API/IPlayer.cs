using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    public interface IPlayer
    {
        /// <summary>
        /// The id of the player
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Which phase of the game is this player currently in
        /// </summary>
        Phase Phase { get; }

        /// <summary>
        /// Initially set the game this player participates on, the id he/she/it has 
        /// and other stuff
        /// </summary>
        void Init(Game game, int id);

        /// <summary>
        /// Notify this instance that it's currently its turn
        /// </summary>
        void BeginTurn(Game game);

        /// <summary>
        /// Notify this instance that it is no longer its turn
        /// </summary>
        void EndTurn(Game game);
    }
}
