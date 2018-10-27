using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Holds information about a players current situation during a game
    /// </summary>
    public class PlayerGameStatus
    {
        /// <summary>
        /// A reference to the player
        /// </summary>
        public IPlayer Player { get; set; }

        /// <summary>
        /// The amount of men the player has placed already
        /// </summary>
        public int MenPlaced { get; private set; }

        /// <summary>
        /// The amount of men that this player currently has on the field
        /// </summary>
        public int MenAlive { get; private set; }

        /// <summary>
        /// C'tor
        /// </summary>
        public PlayerGameStatus(IPlayer player)
        {
            this.Player = player;
            this.MenPlaced = 0; //Setting the value explicitly
        }

        /// <summary>
        /// Call this whenever the player has placed a man
        /// </summary>
        public void PlaceMan() //Extrude the increment/assignment of the MenPlaced variable into a method which makes it irreversible
        {
            MenPlaced++;
            MenAlive++;
        }

        /// <summary>
        /// Call this whenever a man gets killed
        /// </summary>
        public void KillMan()
        {
            MenAlive--;
        }
    }
}
