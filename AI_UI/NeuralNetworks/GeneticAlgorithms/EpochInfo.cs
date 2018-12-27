using AI.NeuralNetworks.GeneticAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_UI.NeuralNetworks.GeneticAlgorithms
{
    /// <summary>
    /// Represents info for one epoch
    /// </summary>
    public struct EpochInfo
    {
        /// <summary>
        /// The time it took to complete the epoch
        /// </summary>
        public TimeSpan Time { get; }

        /// <summary>
        /// The population
        /// </summary>
        public ISpecies[] Population { get; }

        /// <summary>
        /// The size of the population
        /// </summary>
        public int PopulationSize { get; }

        //C'tor
        public EpochInfo(TimeSpan time, ISpecies[] population)
        {
            Time = time;
            Population = population;
            PopulationSize = population.Length;
        }
    }
}
