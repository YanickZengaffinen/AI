using AI.NeuralNetworks.GeneticAlgorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.NeuralNetworks.FeedForward.Learning
{
    public class Species : ISpecies
    {
        public INetwork Network { get; }

        public double CachedScore { get; private set; }

        private MutationParameters mutationParams;

        private Func<Species, double> rankingFunc;

        private bool reEvaluate;
        private bool evaluated = false;

        private double lastScore; //the last score this species scored

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="reEvaluate">Should this species' rank be evaluated every epoch</param>
        /// <param name="rankingFunc">A function to allow the use of external logic to calculate the score (= the return value)</param>
        public Species(INetwork network, in MutationParameters mutationParams, Func<Species, double> rankingFunc, bool reEvaluate = true)
        {
            this.Network = network;
            this.mutationParams = mutationParams;
            this.reEvaluate = reEvaluate;
            this.rankingFunc = rankingFunc;
        }

        public ISpecies Mutate()
        {
            var networkClone = this.Network.Clone();

            networkClone.Mutate(mutationParams);

            return new Species(networkClone, mutationParams, rankingFunc, reEvaluate);
        }

        public double CalculateScore()
        {
            if(reEvaluate || !evaluated)
            {
                evaluated = true;
                lastScore = rankingFunc.Invoke(this);
            }

            return lastScore;
        }
    }
}
