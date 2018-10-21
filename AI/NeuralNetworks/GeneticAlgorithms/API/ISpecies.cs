using System;
using System.Collections.Generic;
using System.Text;

namespace AI.NeuralNetworks.GeneticAlgorithms
{
    public interface ISpecies
    {
        /// <summary>
        /// The score that was last calculated for this species
        /// </summary>
        double Score { get; }

        /// <summary>
        /// Calculates how good this instance should be ranked in the genetic algorithm
        /// </summary>
        /// <returns> A value representing the rank of this instance. Higher values = better ranking </returns>
        double Rank();

        /// <summary>
        /// Mutates this instance to generate offspring with the same traits
        /// </summary>
        /// <returns> A mutation of this instance </returns>
        ISpecies Mutate();
    }
}
