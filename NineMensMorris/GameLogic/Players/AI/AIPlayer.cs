using NineMensMorris.GameLogic.Players.AI;
using NineMensMorris.GameLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    public class AIPlayer : IPlayer
    {
        public int ID { get; private set; }

        private Game game;

        private PlacementController placerNetwork;
        private MoveController moveController;
        private KillController killController;
        private FlyingController flyingController;

        public void Init(PlacementController placerNetwork, MoveController moveController, KillController killController, FlyingController flyingController)
        {
            this.placerNetwork = placerNetwork;
            this.moveController = moveController;
            this.killController = killController;
            this.flyingController = flyingController;
        }

        public void Init(Game game, int id)
        {
            this.ID = id;
            this.game = game;
        }

        public void BeginTurn(Game game)
        {
            switch(game.CheckPhase(this))
            {
                case Phase.Placing:
                    TryActions(game, 
                        placerNetwork.GetRankedActions(game.GetPoints()), 
                        (RatedObject<Placement> ratedPlacement) => game.Place(ratedPlacement.Object));
                    break;
                case Phase.Moving:
                    TryActions(game, 
                        moveController.GetRankedActions(game.GetPoints()),
                        (RatedObject<Move> move) => game.Move(move.Object));
                    break;
                case Phase.Flying:
                    TryActions(game,
                        flyingController.GetRankedActions(game.GetPoints()),
                        (RatedObject<Move> move) => game.Move(move.Object));
                    break;
            }

            //check if there are any kills pending for this player
            while(game.HasKillPending(this))
            {
                TryActions(game,
                    killController.GetRankedActions(game.GetPoints()),
                    (RatedObject<Kill> kill) => game.Kill(kill.Object));
            }
        }

        /// <summary>
        /// Tries to place down a man in the order that is suggested by the neural network
        /// </summary>
        private void Place(Game game)
        {
            var rankedPoints = placerNetwork.GetRankedActions(game.GetPoints());

            foreach(var rankedPoint in rankedPoints)
            {
                if(game.Place(rankedPoint.Object))
                {
                    break;
                }
            }

        }

        /// <summary>
        /// Tries to execute different actions until one is finally successful and returns true
        /// </summary>
        private void TryActions<T>(Game game, IList<RatedObject<T>> objects, Func<RatedObject<T>, bool> func, int maxCount = int.MaxValue)
        {
            for(int i = 0; i < objects.Count && i < maxCount; i++)
            {
                if(func.Invoke(objects[i]))
                {
                    break;
                }
            }
        }

        public void EndTurn(Game game) { }

    }
}
