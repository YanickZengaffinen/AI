namespace AI.Util.RandomNumberGenerators
{
    /// <summary>
    /// Generates a random number
    /// </summary>
    public interface IRandom
    {
        /// <summary>
        /// Generates a random number between 0 and 1
        /// </summary>
        double Generate();

        /// <summary>
        /// Generates a random number between -1 and 1
        /// </summary>
        double GenerateBi();
    }
}
