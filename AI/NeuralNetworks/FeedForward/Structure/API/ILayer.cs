namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// Interface representing a layer of neurons in a neural network
    /// </summary>
    public interface ILayer
    {
        /// <summary>
        /// Get all neurons of this layer
        /// </summary>
        INeuron[] Neurons { get; }

        /// <summary>
        /// Add an indexer to this layer to get a neuron by its index
        /// </summary>
        /// <param name="index"> The index of the neuron </param>
        /// <returns> The neuron </returns>
        INeuron this[int index] { get; }

        /// <summary>
        /// The size of the layer
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Calculates this layer of neurons
        /// </summary>
        void Calculate();

        /// <summary>
        /// Deep clones this layer
        /// </summary>
        ILayer Clone();
    }
}
