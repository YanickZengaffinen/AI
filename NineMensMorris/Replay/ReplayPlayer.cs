using NineMensMorris.GameLogic;

namespace NineMensMorris.Replay
{
    /// <summary>
    /// A mock player for replays
    /// </summary>
    public class ReplayPlayer : IPlayer
    {
        public int ID { get; private set; }

        public void BeginTurn(Game game) { }

        public void EndTurn(Game game) { }

        public void Init(Game game, int id) {
            this.ID = id;
        }
    }
}
