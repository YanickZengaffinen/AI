using AI.NeuralNetworks;
using AI.NeuralNetworks.FeedForward;
using AI.NeuralNetworks.FeedForward.Learning;
using AI.NeuralNetworks.GeneticAlgorithms;
using NineMensMorris.GameLogic;
using NineMensMorris.GameLogic.Players.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GeneticAlgorithms
{
    /// <summary>
    /// Class that represents an AI Player that genetically learns
    /// </summary>
    public class GeneticGameAI : ISpecies
    {
        public double CachedScore { get; private set; }

        private const int maxMoves = 200; //at how many moves will the game be aborted

        private const double 
            winMultiplier = 10000, 
            abortMultiplier = 1000,
            killMultiplier = 1500,
            aliveMultiplier = 1000;

        private const double 
            loseMultiplier = -5000,
            flyMultiplier = -20,
            moveMultiplier = -20;

        private const double 
            falseKillMultiplier = -50,
            falseMoveMultiplier = -50,
            falseFlightMultiplier = -50,
            falsePlacementMultiplier = -50;

        private readonly MutationParameters mutationParameters = new MutationParameters(0.5f, 0.2f, 0.3f, 0.1f);

        private INetwork placementNetwork,
            moveNetwork,
            killNetwork,
            flyingNetwork;

        private GeneticGameAI enemy;


        /// <summary>
        /// C'tor
        /// </summary>
        public GeneticGameAI(INetwork placementNetwork, INetwork moveNetwork, INetwork killNetwork, INetwork flyNetwork)
        {
            this.placementNetwork = placementNetwork;
            this.moveNetwork = moveNetwork;
            this.killNetwork = killNetwork;
            this.flyingNetwork = flyNetwork;
        }

        /// <summary>
        /// Set the enemy player this ai will compete against
        /// </summary>
        public void SetEnemy(GeneticGameAI enemy)
        {
            this.enemy = enemy;
        }

        /// <summary>
        /// Plays one game and returns the score of this AI
        /// </summary>
        public double CalculateScore()
        {
            if (this.enemy == null)
                throw new Exception("Enemy not set. Call SetEnemy first");

            var aiPlayer = new AIPlayer();
            var enemyAIPlayer = new AIPlayer();

            var simulatedGame = new SimulatedGame(enemyAIPlayer, aiPlayer,
                winMultiplier, abortMultiplier, killMultiplier, aliveMultiplier, flyMultiplier, moveMultiplier, loseMultiplier, 
                falseKillMultiplier, falseMoveMultiplier, falseFlightMultiplier, falsePlacementMultiplier, maxMoves);

            OverrideEmptyAIPlayer(aiPlayer, simulatedGame);
            enemy.OverrideEmptyAIPlayer(enemyAIPlayer, simulatedGame);

            return CachedScore = simulatedGame.Simulate();
        }

        /// <summary>
        /// Converts this instance into a AIPlayer
        /// </summary>
        /// <param name="game">Actual game reference required to setup controllers</param>
        /// <param name="emptyPlayer">AIPlayer-shell</param>
        public AIPlayer OverrideEmptyAIPlayer(AIPlayer emptyPlayer, Game game)
        {
            emptyPlayer.Init(
                new PlacementController(placementNetwork, emptyPlayer),
                new MoveController(moveNetwork, emptyPlayer, game.Board.GetAllMovesAdjacents(emptyPlayer)),
                new KillController(killNetwork, emptyPlayer),
                new FlyingController(flyingNetwork, emptyPlayer, game.Board.GetAllMoves(emptyPlayer))
            );

            return emptyPlayer;
        }

        public ISpecies Mutate()
        {
            var placementClone = this.placementNetwork.Clone();
            placementClone.Mutate(mutationParameters);

            var moveClone = this.moveNetwork.Clone();
            moveClone.Mutate(mutationParameters);

            var killClone = this.killNetwork.Clone();
            killClone.Mutate(mutationParameters);

            var flyClone = this.flyingNetwork.Clone();
            flyClone.Mutate(mutationParameters);


            return new GeneticGameAI(placementClone, moveClone, killClone, flyClone);
        }
    }
}
