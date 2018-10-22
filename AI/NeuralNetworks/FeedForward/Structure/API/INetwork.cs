using System.Collections.Generic;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// Interface representing a neural network
    /// </summary>
    public interface INetwork
    {
        /// <summary>
        /// Get all the hidden layers of the net
        /// </summary>
        ILayer[] HiddenLayers { get; }

        /// <summary>
        /// The input layer of this net
        /// </summary>
        ILayer InputLayer { get; }

        /// <summary>
        /// The output layer of this net
        /// </summary>
        ILayer OutputLayer { get; }
        
        /// <summary>
        /// A list of all the neurons of the layer
        /// </summary>
        IList<INeuron> Neurons { get; } //TODO: test performance of IList vs LinkedList

        /// <summary>
        /// A list of all the synapses of the layer
        /// </summary>
        IList<ISynapse> Synapses { get; }

        /// <summary>
        /// Indexer for this network
        /// </summary>
        /// <param name="id">The id of the neuron to look for</param>
        /// <returns>The neuron with the given id</returns>
        INeuron this[int id] { get; }

        /// <summary>
        /// Calculates the entire network from the given array of input values
        /// </summary>
        /// <param name="inputValues"> 
        /// The values that will be inputted into the neural network. 
        /// There must be as many values as there are input neurons! 
        /// </param>
        void Calculate(double[] inputValues);

        /// <summary>
        /// Deep clones this network
        /// </summary>
        INetwork Clone();
    }
}
