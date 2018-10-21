namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// A feed forward synapse
    /// </summary>
    public class Synapse : ISynapse
    {
        public double Weight { get; set; }

        public INeuron Sender { get; }
        public INeuron Receiver { get; }

        /// <summary>
        /// Basic c'tor
        /// </summary>
        public Synapse(INeuron sender, INeuron receiver, in double weight)
        {
            this.Sender = sender;
            this.Receiver = receiver;

            this.Weight = weight;
        }

        public ISynapse Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
