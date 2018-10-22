using AI.NeuralNetworks.FeedForward;
using AI.Util.RandomNumberGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.NeuralNetworks
{
    /// <summary>
    /// Class that helps mutating feed forward networks of type: <see cref="AI.NeuralNetworks.FeedForward.INetwork"/>
    /// </summary>
    public static partial class NetworkMutator
    {
        private const double ZERO = 1e-5; //a constant value representing zero

        private static readonly IRandom random = new StandardRandom();
        
        /// <summary>
        /// Changes the weights of synapses in a network
        /// </summary>
        /// <param name="chance"> By what chance will each synapse be mutated </param>
        /// <param name="deviation"> 
        /// The max difference in percent from the original value.
        /// If the weight is too close to zero a strength of <see cref="ZERO"/> will be assumed.
        /// </param>
        public static void MutateWeights(INetwork network, in float chance, in float deviation)
        {
            foreach(var synapse in network.Synapses)
            {
                ChangeValue(synapse.Weight, chance, deviation, (double val) => { synapse.Weight = val; });
            }
        }

        /// <summary>
        /// Changes the biases of neurons
        /// </summary>
        /// <param name="chance"> The chance that a mutation happens </param>
        /// <param name="deviation"> By how much should the value be mutated... usually a very small value </param>
        public static void MutateBias(INetwork network, in float chance, in float deviation)
        {
            foreach(var neuron in network.Neurons)
            {
                ChangeValue(neuron.Bias, chance, deviation, (double val) => { neuron.Bias = val; });
            }
        }

        /// <summary>
        /// Change a value by a certain chance by a max amount of deviation
        /// </summary>
        /// <param name="value"> The current value </param>
        /// <param name="chance"> The chance by which a mutation will happen </param>
        /// <param name="deviation"> The maximum amount by which the value will change </param>
        /// <param name="onChange"> Response function that gets called whenever the value is being changed. Takes in the changed value as an argument. </param>
        private static void ChangeValue(in double value, in float chance, in float deviation, Action<double> onChange)
        {
            if (random.Generate() <= chance) //check if a mutation should happenha
            {
                var deviationPercent = deviation * random.GenerateBi();

                if (Math.Abs(value) < ZERO) //consider the neuron bias to be 0 --> assume ZERO
                {
                    var newAbsBias = ZERO * (1 + deviationPercent);
                    onChange.Invoke(value < 0 ? -newAbsBias : newAbsBias);
                }
                else
                {
                    onChange.Invoke(value * (1 + deviationPercent));
                }
            }
        }
    }
}
