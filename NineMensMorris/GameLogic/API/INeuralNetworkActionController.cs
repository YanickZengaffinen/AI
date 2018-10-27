using AI.NeuralNetworks.FeedForward;
using NineMensMorris.GameLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Abstract class that represents a controller which ranks actions
    /// </summary>
    public abstract class ANeuralNetworkActionController<T>
    {
        /// <summary>
        /// Reference to the player that this controller serves
        /// </summary>
        protected IPlayer Player { get; }

        /// <summary>
        /// The network that does the ranking
        /// </summary>
        public INetwork NeuralNetwork { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        protected ANeuralNetworkActionController(INetwork network, IPlayer player)
        {
            NeuralNetwork = network;
            Player = player;
        }

        /// <summary>
        /// Get a assessment of the current situation and evaluate the best moves.
        /// </summary>
        public IList<RatedObject<T>> GetRankedActions(Point[] inputs)
        {
            var rVal = new List<RatedObject<T>>(NeuralNetwork.OutputLayer.Size);

            //feeds the network with the given input and calculates the outputs
            NeuralNetwork.Calculate(inputs.Select(x => (double)x.OwnerId).ToArray());

            var outputNeurons = NeuralNetwork.OutputLayer.Neurons;

            //map all the output neuron values
            for (int i = 0; i < outputNeurons.Count; i++)
            {
                rVal.Add(Map(inputs, i, outputNeurons[i].Value));
            }

            return rVal.OrderBy(x => x.Rating).ToList();
        }

        /// <summary>
        /// Maps the value and the object for a specific index.
        /// </summary>
        /// <param name="inputs"> A reference to the points of the board </param>
        public abstract RatedObject<T> Map(Point[] inputs, in int index, in double value);

    }
}
