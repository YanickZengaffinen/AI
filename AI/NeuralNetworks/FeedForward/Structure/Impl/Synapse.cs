namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// A feed forward synapse
    /// </summary>
    [System.Serializable]
    public class Synapse : ISynapse
    {
        public double Weight { get; set; }

        public int SenderId { get; }
        public int ReceiverId { get; }

        /// <summary>
        /// Basic c'tor
        /// </summary>
        public Synapse(in int senderId, in int receiverId, in double weight)
        {
            this.SenderId = senderId;
            this.ReceiverId = receiverId;

            this.Weight = weight;
        }

        public ISynapse Clone()
        {
            return new Synapse(SenderId, ReceiverId, Weight);
        }
    }
}
