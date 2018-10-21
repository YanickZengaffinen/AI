using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// Fully connected feed forward neural network
    /// </summary>
    [System.Serializable]
    public class Network : INetwork
    {
        public ILayer InputLayer { get; }
        public ILayer[] HiddenLayers { get; }
        public ILayer OutputLayer { get; }

        public IList<INeuron> Neurons { get; }

        public Network(ILayer inputLayer, ILayer[] hiddenLayers, ILayer outputLayer)
        {
            this.InputLayer = inputLayer;
            this.HiddenLayers = hiddenLayers;
            this.OutputLayer = outputLayer;

            this.Neurons = InputLayer.Neurons.Concat(OutputLayer.Neurons).Concat(HiddenLayers.SelectMany(x => x.Neurons)).ToList();
        }

        public INeuron this[int index]
        {
            get { return Neurons.First(x => x.Id == index); }
        }

        public void Calculate(double[] inputValues)
        {
            //set all the values of the input layer
            for(int i = 0; i < InputLayer.Neurons.Count; i++)
            {
                InputLayer.Neurons[i].Value = inputValues[i];
            }

            //calculate the hidden layers... layer by layer
            for(int i = 0; i < HiddenLayers.Length; i++)
            {
                HiddenLayers[i].Calculate(this);
            }

            //calculate the output layer
            OutputLayer.Calculate(this);
        }

        public INetwork Clone()
        {
            return new Network(InputLayer.Clone(), HiddenLayers.Select(x => x.Clone()).ToArray(), OutputLayer.Clone());
        }
    }
}
