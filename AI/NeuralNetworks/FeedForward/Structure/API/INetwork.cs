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

        /// <summary>
        /// Gets a neuron by its id
        /// </summary>
        INeuron GetNeuronById(int id); //cannot use indexer because index and id could be mixed up

    }
}
