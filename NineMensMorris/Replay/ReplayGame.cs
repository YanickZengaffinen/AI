using NineMensMorris.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.Replay
{
    /// <summary>
    /// Game with no internal logic which simply executes actions
    /// </summary>
    public class ReplayGame : IGame
    {
        public Board Board { get; private set; } = new Board(Game.HostId);

        public event EventHandler<Placement> onPlaced;
        public event EventHandler<Move> onMoved;
        public event EventHandler<Kill> onKilled;

        /// <summary>
        /// Finds out the type of an action and executes the corresponding method
        /// </summary>
        public void ExecuteAction(IAction action)
        {
            if (action is Move)
            {
                Move((Move)action);
            }
            else if (action is Placement)
            {
                Place((Placement)action);
            }
            else if(action is Kill)
            {
                Kill((Kill)action);
            }
        }

        public bool Kill(Kill kill)
        {
            Board[kill.Target] = Game.HostId;

            onKilled?.Invoke(this, kill);

            return true;
        }

        public bool Move(Move move)
        {
            Board[move.Start] = Game.HostId;
            Board[move.Destination] = move.Player.ID;

            onMoved?.Invoke(this, move);

            return true;
        }

        public bool Place(Placement placement)
        {
            Board[placement.Target] = placement.Player.ID;

            onPlaced?.Invoke(this, placement);

            return true;
        }
    }
}
