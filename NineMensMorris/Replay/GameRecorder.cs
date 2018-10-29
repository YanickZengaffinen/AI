using Logging;
using NineMensMorris.GameLogic;

namespace NineMensMorris.Replay
{
    /// <summary>
    /// Records a game
    /// </summary>
    public class GameRecorder : Logger<IAction>
    {
        public GameRecorder(Game game)
        {
            game.onPlaced += (object s, Placement placement) => AddLog(placement);
            game.onMoved += (object s, Move move) => AddLog(move);
            game.onKilled += (object s, Kill kill) => AddLog(kill);
        }
    }
}
