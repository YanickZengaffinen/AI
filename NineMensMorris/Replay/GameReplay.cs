using Logging;
using NineMensMorris.GameLogic;
using System.Collections.Generic;

namespace NineMensMorris.Replay
{
    /// <summary>
    /// Logger that logs game actions
    /// </summary>
    public class GameReplay
    {
        public ReplayGame Game { get; }

        private IEnumerator<ILogEntry<IAction>> enumerator;
        
        public GameReplay(GameRecorder recorder)
        {
            Game = new ReplayGame();

            enumerator = recorder.Log.GetEnumerator();
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
