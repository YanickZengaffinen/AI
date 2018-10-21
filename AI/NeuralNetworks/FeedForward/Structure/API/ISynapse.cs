namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// Interface for a connection between two neurons
    /// </summary>
    public interface ISynapse
    {
        /// <summary>
        /// The strength with which this synapse transmits the signal to the next neuron
        /// </summary>
        double Weight { get; set; }

        /// <summary>
        /// The start of this synapse
        /// </summary>
        int SenderId { get; }

        /// <summary>
        /// The end of this synapse
        /// </summary>
        int ReceiverId { get; }

        /// <summary>
        /// Deep clones this synapse
        /// </summary>
        ISynapse Clone();
    }
}
