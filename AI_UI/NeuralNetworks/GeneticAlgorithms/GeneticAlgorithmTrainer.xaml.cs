using AI.NeuralNetworks.GeneticAlgorithms;
using PerformanceTesting.Pseudo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AI_UI.NeuralNetworks.GeneticAlgorithms
{
    /// <summary>
    /// Interaction logic for GeneticAlgorithmTrainer.xaml
    /// </summary>
    public partial class GeneticAlgorithmTrainerWindow : Window
    {
        /// <summary>
        /// Raised whenever the user clicks on an epoch
        /// </summary>
        public event EventHandler onEpochSelected;

        /// <summary>
        /// Raised whenever the user clicks on a species in the epoch detail view
        /// </summary>
        public event EventHandler onSpeciesSelected;

        /// <summary>
        /// Raised before any internal logic whenever the user clicks on "Next Epoch"-button.
        /// </summary>
        public event EventHandler onNextEpochClick;

        //The genetic algorithm
        private IGeneticAlgorithm geneticAlgorithm;

        private List<ISpecies[]> epochs = new List<ISpecies[]>();

        public GeneticAlgorithmTrainerWindow(IGeneticAlgorithm geneticAlgorithm)
        {
            InitializeComponent();

            this.geneticAlgorithm = geneticAlgorithm;

            StorePopulation();
        }

        /// <summary>
        /// Stores the state of the genetic algorithm... usually done once per epoch
        /// </summary>
        private void StorePopulation()
        {
            //add an entry to the listView
            ListView_EpochOverview.Items.Add("#" + epochs.Count);

            //store a copy of the algorithms population
            epochs.Add(geneticAlgorithm.Population.ToArray());
        }

        //Another epoch has been selected
        private void EpochOverview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var epoch = epochs[ListView_EpochOverview.SelectedIndex];
            var detailItems = this.ListView_EpochDetail.Items;

            detailItems.Clear();

            //add an entry for each species of the selected epoch
            for(int i = 0; i < epoch.Length; i++)
            {
                detailItems.Add($"#{i} ({epoch[i].CachedScore})");
            }
            
            //raise event
            onEpochSelected?.Invoke(this, EventArgs.Empty);
        }

        //A different species has been selected
        private void EpochDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //raise event
            onSpeciesSelected?.Invoke(this, EventArgs.Empty);
        }

        //simulate the next epoch
        private void NextEpoch_Click(object sender, RoutedEventArgs e)
        {
            onNextEpochClick?.Invoke(this, EventArgs.Empty);

            long epochTest = PseudoPerformanceTester.From();
            geneticAlgorithm.DoEpoch();

            Console.WriteLine(PseudoPerformanceTester.To(epochTest));

            StorePopulation();
        }
    }
}
