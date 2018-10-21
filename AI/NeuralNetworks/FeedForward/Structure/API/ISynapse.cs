using System;
using System.Collections.Generic;
using System.Text;

namespace AI.NeuralNetworks.FeedForward
{
    public interface ISynapse
    {
        /// <summary>
        /// The strength with which this synapse transmits the signal to the next neuron
        /// </summary>
        double Weight { get; set; }

        /// <summary>
        /// The start of this synapse
        /// </summary>
        INeuron Sender { get; }

        /// <summary>
        /// The end of this synapse
        /// </summary>
        INeuron Receiver { get; }
    }
}
