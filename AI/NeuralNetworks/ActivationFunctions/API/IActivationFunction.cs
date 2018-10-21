namespace AI.NeuralNetworks.ActivationFunctions
{
    /// <summary>
    /// A mathematical function
    /// </summary>
    public interface IActivationFunction
    {
        /// <summary>
        /// Executes a mathematical function on a value
        /// </summary>
        /// <param name="value"> The start value </param>
        /// <returns> The calculated value </returns>
        double Calculate(in double value);

    }
}
