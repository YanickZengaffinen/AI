using AI.NeuralNetworks.ActivationFunctions;
using AI.NeuralNetworks.FeedForward;
using AI_Test.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Test.NeuralNetworks.Tests
{
    /// <summary>
    /// Test that checks whether or not the FeedForwardNeuralNetwork can be initialized and calculated without throwing any error
    /// </summary>
    internal class FeedForwardNeuralNetworkTester : ATest
    {
        public FeedForwardNeuralNetworkTester(string name) : base(name) { }

        protected override void Execute()
        {
            INetwork fcffNetwork = NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                new SigmoidFunction(),
                5,
                10,
                2, 3
            );

            fcffNetwork.Calculate(new double[] { .5, 2, .4, -.3, 0});

            Console.ReadLine();
        }
    }
}
