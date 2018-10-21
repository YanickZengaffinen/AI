using AI.NeuralNetworks.ActivationFunctions;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// Interface representing a neuron in a neural network
    /// </summary>
    public interface INeuron
    {
        /// <summary>
        /// The id of this neuron
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The last value this neuron had
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Get the bias of this neuron
        /// </summary>
        double Bias { get; set; }

        /// <summary>
        /// The activation function of this neuron
        /// </summary>
        IActivationFunction ActivationFunction { get; }

        /// <summary>
        /// The synapses that end at this neuron
        /// </summary>
        ISynapse[] IncomingSynapses { get; set; }

        /// <summary>
        /// The synapses that start at this neuron
        /// </summary>
        ISynapse[] OutgoingSynapses { get; set; }

        /// <summary>
        /// Calculates the value of this neuron
        /// </summary>
        /// <param name="network">The network that holds this neuron</param>
        void Calculate(INetwork network);

        /// <summary>
        /// Deep clones this neuron
        /// </summary>
        INeuron Clone();
    }
}
