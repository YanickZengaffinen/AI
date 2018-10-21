using System;

namespace AI.NeuralNetworks.ActivationFunctions
{
    /// <summary>
    /// Take a look at https://en.wikipedia.org/wiki/Sigmoid_function
    /// </summary>
    public class SigmoidFunction : IActivationFunction
    {
        public double Calculate(in double value)
        {
            return 1 / (1 + Math.Pow(Math.E, -value));
        }
    }
}
