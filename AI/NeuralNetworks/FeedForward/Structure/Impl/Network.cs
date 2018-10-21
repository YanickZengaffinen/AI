namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// Fully connected feed forward neural network
    /// </summary>
    public class Network : INetwork
    {
        public ILayer InputLayer { get; }
        public ILayer[] HiddenLayers { get; }
        public ILayer OutputLayer { get; }
        
        public Network(ILayer inputLayer, ILayer[] hiddenLayers, ILayer outputLayer)
        {
            this.InputLayer = inputLayer;
            this.HiddenLayers = hiddenLayers;
            this.OutputLayer = outputLayer;
        }

        public void Calculate(double[] inputValues)
        {
            //set all the values of the input layer
            for(int i = 0; i < InputLayer.Neurons.Length; i++)
            {
                InputLayer.Neurons[i].Value = inputValues[i];
            }

            //calculate the hidden layers... layer by layer
            for(int i = 0; i < HiddenLayers.Length; i++)
            {
                HiddenLayers[i].Calculate();
            }

            //calculate the output layer
            OutputLayer.Calculate();
        }
    }
}
