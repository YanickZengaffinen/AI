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
        /// Event that gets called whenever an epoch has been finished
        /// </summary>
        event EventHandler onEpochFinished;

        /// <summary>
        /// Event that raises whenever all the species scores have been calculated during an epoch
        /// </summary>
        event EventHandler onScoresCalculated;

        /// <summary>
        /// Simulates all the species, lets the top few percent survive and breeds them
        /// </summary>
        void DoEpoch();

    }
}
