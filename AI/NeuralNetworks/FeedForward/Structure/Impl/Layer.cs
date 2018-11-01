using AI.Util;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// A feed forward layer
    /// </summary>
    [System.Serializable]
    public class Layer : ILayer
    {
        public IList<INeuron> Neurons { get; }

        public int Size => Neurons.Count;

        /// <summary>
        /// Simple c'tor
        /// </summary>
        public Layer(params INeuron[] neurons)
        {
            this.Neurons = neurons;
        }

        public INeuron this[int index]
        {
            get { return Neurons[index]; }
        }

        public void Calculate(INetwork network)
        {
            //Calculates the values of this layers neurons
            Neurons.AsParallel().ForAll(x => x.Calculate(network));
        }

        public ILayer Clone()
        {
            return new Layer(Neurons.SelectArray(x => x.Clone()));
        }
    }
}
