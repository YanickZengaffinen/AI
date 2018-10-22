using AI.NeuralNetworks.GeneticAlgorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.NeuralNetworks.FeedForward.Learning
{
    public class Species : ISpecies
    {
        public INetwork Network { get; }

        public double Score { get; private set; }

        private MutationParameters mutationParams;

        private Func<Species, double> rankingFunc;

        private bool reEvaluate;
        private bool evaluated = false;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="network">The network that is used to calculate the rank of this species</param>
        /// <param name="reEvaluate">Should this species' rank be evaluated every epoch</param>
        /// <param name="rankingFunc">A function to allow the use of external logic to calculate the score</param>
        public Species(INetwork network, in MutationParameters mutationParams, Func<Species, double> rankingFunc, bool reEvaluate = true)
        {
            this.Network = network;
            this.mutationParams = mutationParams;
            this.reEvaluate = reEvaluate;
            this.rankingFunc = rankingFunc;
        }

        public ISpecies Mutate()
        {
            var mutatedNetwork = this.Network.Clone();

            //no structural mutations allowed for fully connected feed forward neural networks
            NetworkMutator.MutateBias(Network, mutationParams.BiasChance, mutationParams.BiasDeviation);
            NetworkMutator.MutateWeights(Network, mutationParams.WeightChance, mutationParams.WeightDeviation);

            return new Species(mutatedNetwork, mutationParams, rankingFunc, reEvaluate);
        }

        public void CalculateScore()
        {
            if(reEvaluate || !evaluated)
            {
                Score = rankingFunc.Invoke(this);
                evaluated = true;
            }
        }
    }
}
