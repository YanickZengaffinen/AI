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

        private Func<Species, double> rankingFunc;

        private bool reEvaluate;
        private bool evaluated = false;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="network">The network that is used to calculate the rank of this species</param>
        /// <param name="reEvaluate">Should this species' rank be evaluated every epoch</param>
        /// <param name="rankingFunc">A function to allow the use of external logic to calculate the score</param>
        public Species(INetwork network, Func<Species, double> rankingFunc, bool reEvaluate = true)
        {
            this.Network = network;
            this.reEvaluate = reEvaluate;
            this.rankingFunc = rankingFunc;
        }

        public ISpecies Mutate()
        {
            var mutatedNetwork = this.Network;

            //TODO: mutate this species
            //no structural mutations allowed for fully connected feed forward neural networks

            return new Species(mutatedNetwork, rankingFunc, reEvaluate);
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
