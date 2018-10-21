using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
