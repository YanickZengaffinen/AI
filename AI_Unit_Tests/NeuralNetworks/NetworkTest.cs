using AI.NeuralNetworks.ActivationFunctions;
using AI.NeuralNetworks.FeedForward;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AI_Unit_Tests.NeuralNetworks
{
    public class NetworkTest
    {

        [Fact]
        public void CalculationTest()
        {
            var neuronA = new Neuron(0, 1.5d, SigmoidFunction.Instance);
            var neuronB = new Neuron(1, -0.5d, SigmoidFunction.Instance);

            var aToC = new Synapse(0, 2, 0.5);
            var bToC = new Synapse(1, 2, -1);

            neuronA.OutgoingSynapses = new ISynapse[] { aToC };
            neuronB.OutgoingSynapses = new ISynapse[] { bToC };

            var neuronC = new Neuron(2, 1d, SigmoidFunction.Instance);
            neuronC.IncomingSynapses = new ISynapse[] { aToC, bToC };

            var network = new Network(new Layer(neuronA, neuronB), new ILayer[0], new Layer(neuronC));
            network.Calculate(new double[] { -2, 1.5 });

            Assert.True(Math.Abs(network.GetNeuronById(2).Value - 0.182426d) < 0.0001d, "Failed to feed forward values!");
        }

    }
}
