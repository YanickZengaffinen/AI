namespace AI.NeuralNetworks.GeneticAlgorithms
{
    /// <summary>
    /// Immutable class that holds chances and deviations for any mutations on neural networks
    /// </summary>
    public struct MutationParameters
    {
        /// <summary>
        /// The chance for each weight to be mutated
        /// </summary>
        public float WeightChance { get; }

        /// <summary>
        /// By how much should the weight be changed at max
        /// </summary>
        public float WeightDeviation { get; }


        /// <summary>
        /// The chance for each bias to be mutated
        /// </summary>
        public float BiasChance { get; }

        /// <summary>
        /// By how much should the bias be changed at max
        /// </summary>
        public float BiasDeviation { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public MutationParameters(float weightChance, float weightDeviation, 
            float biasChance, float biasDeviation)
        {
            this.WeightChance = weightChance;
            this.WeightDeviation = weightDeviation;
            this.BiasChance = biasChance;
            this.BiasDeviation = biasDeviation;
        }
        
        //TODO: add "addNeuron", "removeNeuron", "addSynapsis", "removeSynapsis" properties. Maybe in StructuralMutationParameters : MutationParameters

    }
}
