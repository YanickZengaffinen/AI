using System;
using System.Collections.Generic;
using System.Text;

namespace AI.NeuralNetworks.GeneticAlgorithms
{
    public interface IGeneticAlgorithm
    {
        /// <summary>
        /// The amount of members that can co-exist
        /// </summary>
        int PopulationSize { get; }

        /// <summary>
        /// How many members will survive at the end of each epoch
        /// </summary>
        int SurvivingMembers { get; }

        /// <summary>
        /// A list of all the members currently alive
        /// </summary>
        ISpecies[] Population { get; }

        /// <summary>
        /// Simulates all the species, lets the top few percent survive and breeds them
        /// </summary>
        void DoEpoch();

    }
}
