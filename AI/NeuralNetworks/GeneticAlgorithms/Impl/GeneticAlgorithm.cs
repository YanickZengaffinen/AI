using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.NeuralNetworks.GeneticAlgorithms
{
    public class GeneticAlgorithm : IGeneticAlgorithm
    {
        public int PopulationSize { get; }

        public int SurvivingMembers { get; }

        public ISpecies[] Population => members.ToArray();

        private LinkedList<ISpecies> members = new LinkedList<ISpecies>();
        
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="populationSize"> The amount of members that will be simulated </param>
        /// <param name="starter"> A member to create a start population from </param>
        /// <param name="survivalThreshold"> The amount of the population that will survive after each epoch (in percent). Must be less than 0.5! </param>
        public GeneticAlgorithm(int populationSize, ISpecies starter, float survivalThreshold)
        {
            this.SurvivingMembers = (int)Math.Round(populationSize * survivalThreshold);

            //Create the initial population which equals the member and n - 1 mutations of it
            members.AddFirst(starter);
            for(int i = 1; i < populationSize; i++)
            {
                members.AddFirst(starter.Mutate());
            }
        }

        public void DoEpoch()
        {
            foreach(var ranker in members)
            {
                ranker.CalculateScore();
            }

            BreedFittest(KillLeastFit());
        }

        /// <summary>
        /// Kills the least fit members of the population
        /// </summary>
        /// <returns>The enumeration of members that have survived</returns>
        protected virtual IEnumerable<ISpecies> KillLeastFit()
        {
            foreach(var member in members)
            {
                member.CalculateScore();
            }

            return members.ToList().OrderBy(x => x.Score).Take(SurvivingMembers);
        }

        /// <summary>
        /// Breed the fittest members
        /// </summary>
        protected virtual void BreedFittest(IEnumerable<ISpecies> survivors)
        {
            //the population space that will be left once all members have duplicated
            int openSpace = PopulationSize - SurvivingMembers * 2;
            double totalScore = survivors.Sum(x => x.Score); //the total score of all surviving members

            for(int i = 0; i < SurvivingMembers; i++)
            {
                var member = survivors.ElementAt(i);

                //add the member itself
                members.AddLast(member);

                //add a mutation from each surviving member
                members.AddLast(member.Mutate());

                //share open population space between the survivors taking into account their fitness
                int budget = (int)(openSpace * member.Score);
                for (int j = 0; j < budget; j++)
                {
                    members.AddLast(member.Mutate());
                }
            }

            //fill up the population
            int left = PopulationSize - members.Count();
            for (int i = 0; i < left; i++)
            {
                members.AddLast(survivors.ElementAt(i).Mutate());
            }
        }
    }
}
