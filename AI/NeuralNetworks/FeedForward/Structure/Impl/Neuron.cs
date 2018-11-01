using AI.NeuralNetworks.ActivationFunctions;
using AI.Util;
using System.Linq;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// A feed forward neuron
    /// </summary>
    [System.Serializable]
    public class Neuron : INeuron
    {
        public int Id { get; }

        public double Value { get; set; }

        public double Bias { get; set; }

        public IActivationFunction ActivationFunction { get; }

        public ISynapse[] IncomingSynapses { get; set; }
        public ISynapse[] OutgoingSynapses { get; set; }


        /// <summary>
        /// Very basic c'tor
        /// </summary>
        public Neuron(in int id, in double bias, ISynapse[] incomingSynapses, ISynapse[] outgoingSynapses, IActivationFunction activationFunction)
            : this(id, bias, activationFunction)
        {
            this.IncomingSynapses = incomingSynapses;
            this.OutgoingSynapses = outgoingSynapses;
        }

        /// <summary>
        /// Create a neuron which isn't connected to any other neurons
        /// </summary>
        public Neuron(in int id, in double bias, IActivationFunction activationFunction)
        {
            this.Id = id;
            this.Bias = bias;
            this.ActivationFunction = activationFunction;
        }

        public void Calculate(INetwork network)
        {
            Value = ActivationFunction.Calculate(
                Bias + 
                IncomingSynapses.Sum(x => x.Weight * network.GetNeuronById(x.SenderId).Value)
            );
        }

        public INeuron Clone()
        {
            return new Neuron(
                Id, Bias, 
                IncomingSynapses?.SelectArray(x => x.Clone()), 
                OutgoingSynapses?.SelectArray(x => x.Clone()), 
                ActivationFunction
            );
        }
    }
}
