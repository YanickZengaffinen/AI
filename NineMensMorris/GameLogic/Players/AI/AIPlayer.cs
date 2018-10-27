using NineMensMorris.GameLogic.Players.AI;
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

        private PlacerNN placerNetwork;

        public AIPlayer(PlacerNN placerNetwork)
        {
            this.placerNetwork = placerNetwork;
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
                    Place(game);
                    break;
            }
        }

        /// <summary>
        /// Tries to place down a man in the order that is suggested by the neural network
        /// </summary>
        private void Place(Game game)
        {
            var rankedPoints = placerNetwork.GetRankedPoints(game.GetPoints());

            foreach(var rankedPoint in rankedPoints)
            {
                if(game.Place(new Placement(this, rankedPoint.Item1.Position)))
                {
                    break;
                }
            }
        }

        public void EndTurn(Game game) { }

    }
}
