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
        double Calculate(in double value);

    }
}
