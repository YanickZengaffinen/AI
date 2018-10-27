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
        public IList<ISynapse> Synapses { get; }

        public Network(ILayer inputLayer, ILayer[] hiddenLayers, ILayer outputLayer)
        {
            this.InputLayer = inputLayer;
            this.HiddenLayers = hiddenLayers;
            this.OutputLayer = outputLayer;

            this.Neurons = InputLayer.Neurons.Concat(OutputLayer.Neurons).Concat(HiddenLayers.SelectMany(x => x.Neurons)).ToList();

            this.Synapses = InputLayer.Neurons.SelectMany(x => x.OutgoingSynapses ?? new ISynapse[0]).ToList();

            foreach (var hiddenLayer in HiddenLayers)
            {
                this.Synapses.Concat(hiddenLayer.Neurons.SelectMany(x => x.OutgoingSynapses));
            }

            //Synapses won't be added twice since we only collect outgoing synapses
            //this.Synapses = this.Synapses.Distinct().ToList();
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


        public INeuron GetNeuronById(int id) => Neurons.First(x => x.Id == id);
    }
}
