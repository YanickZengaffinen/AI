using System;
using System.Collections.Generic;
using System.Text;
using AI.NeuralNetworks.ActivationFunctions;
using System.Linq;
using AI.Util;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// A feed forward neuron
    /// </summary>
    public class Neuron : INeuron
    {
        public double Value { get; set; }

        public double Bias { get; set; }

        public IActivationFunction ActivationFunction { get; }

        public ISynapse[] IncomingSynapses { get; set; }
        public ISynapse[] OutgoingSynapses { get; set; }

        /// <summary>
        /// Very basic c'tor
        /// </summary>
        public Neuron(double bias, ISynapse[] incomingSynapses, ISynapse[] outgoingSynapses, IActivationFunction activationFunction)
            : this(bias, activationFunction)
        {
            this.IncomingSynapses = incomingSynapses;
            this.OutgoingSynapses = outgoingSynapses;
        }

        /// <summary>
        /// Create a neuron which isn't connected to any other neurons
        /// </summary>
        public Neuron(double bias, IActivationFunction activationFunction)
        {
            this.Bias = bias;
            this.ActivationFunction = activationFunction;
        }

        public void Calculate()
        {
            Value = ActivationFunction.Calculate(
                Bias + 
                IncomingSynapses.Sum(x => x.Weight * x.Sender.Value)
            );
        }
    }
}
