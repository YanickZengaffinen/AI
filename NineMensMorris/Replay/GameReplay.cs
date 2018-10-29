using Logging;
using NineMensMorris.GameLogic;
using System.Collections.Generic;

namespace NineMensMorris.Replay
{
    /// <summary>
    /// Logger that logs game actions
    /// </summary>
    public class GameReplay : Logger<IAction>
    {
        public Game Game { get; }

        private Dictionary<int, ReplayPlayer> players;

        private IEnumerator<ILogEntry<IAction>> enumerator;
        
        public GameReplay(GameRecorder recorder)
        {
            var playerA = new ReplayPlayer();
            var playerB = new ReplayPlayer();

            players = new Dictionary<int, ReplayPlayer>() {
                { Game.PlayerAId, playerA },
                { Game.PlayerBId, playerB }
            };

            Game = new Game(playerA, playerB);

            enumerator = Log.GetEnumerator();
        }

        /// <summary>
        /// Does the next move
        /// </summary>
        public void Next()
        {
            if(enumerator.MoveNext())
            {
                var action = enumerator.Current.Action;

                if(action is Placement)
                {
                    Game.Place((Placement)action);
                }
                else if(action is Move)
                {
                    Game.Move((Move)action);
                }
                else if(action is Kill)
                {
                    Game.Kill((Kill)action);
                }
            }
        }



    }
}
