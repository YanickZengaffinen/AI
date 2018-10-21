using System.Linq;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// A feed forward layer
    /// </summary>
    public class Layer : ILayer
    {
        public INeuron[] Neurons { get; }

        public int Size { get; }

        /// <summary>
        /// Simple c'tor
        /// </summary>
        public Layer(params INeuron[] neurons)
        {
            this.Neurons = neurons;
            Size = neurons.Length;
        }

        public INeuron this[int index]
        {
            get { return Neurons[index]; }
        }

        public void Calculate()
        {
            //Calculates the values of this layers neurons
            Neurons.AsParallel().ForAll(x => x.Calculate());
        }

        public ILayer Clone()
        {
            return new Layer(Neurons.Select(x => x.Clone()).ToArray());
        }
    }
}
