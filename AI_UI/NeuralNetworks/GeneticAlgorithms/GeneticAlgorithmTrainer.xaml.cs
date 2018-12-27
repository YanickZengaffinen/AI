using AI.NeuralNetworks.GeneticAlgorithms;
using PerformanceTesting.Pseudo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
        public event EventHandler<ISpecies> onSpeciesSelected;

        /// <summary>
        /// Raised before any internal logic whenever the user clicks on "Next Epoch"-button.
        /// </summary>
        public event EventHandler onNextEpochClick;

        //The genetic algorithm
        private IGeneticAlgorithm geneticAlgorithm;

        private List<EpochInfo> epochs = new List<EpochInfo>();

        private ISpecies[] epochStartSpecies; //temporary variable with all the simulated species of an epoch

        public GeneticAlgorithmTrainerWindow(IGeneticAlgorithm geneticAlgorithm)
        {
            InitializeComponent();

            this.geneticAlgorithm = geneticAlgorithm;

            geneticAlgorithm.onScoresCalculated += (o, a) => { epochStartSpecies = geneticAlgorithm.Population.ToArray(); };
        }

        /// <summary>
        /// Stores the state of the genetic algorithm... usually done once per epoch
        /// </summary>
        /// <param name="species"> Should be an immutable array! </param>
        private void StorePopulation(ISpecies[] species, TimeSpan simulationTime)
        {
            //add an entry to the listView
            ListView_EpochOverview.Items.Add("#" + (epochs.Count + 1));

            var epochInfo = new EpochInfo(simulationTime, species);

            //store a copy of the algorithms population
            epochs.Add(epochInfo);
        }

        //Another epoch has been selected
        private void EpochOverview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var epoch = epochs[ListView_EpochOverview.SelectedIndex];

            //show the epoch
            ShowEpochInfo(epoch);

            //show the species
            ShowEpochDetail(epoch);
            
            //raise event
            onEpochSelected?.Invoke(this, EventArgs.Empty);
        }

        //Paints the info on the screen
        private void ShowEpochInfo(EpochInfo info)
        {
            Label_EpochTime.Content = info.Time.ToString();
            Label_EpochSize.Content = info.PopulationSize;
        }

        //Updates the species list of the current selected epoch
        private void ShowEpochDetail(EpochInfo info)
        {
            var detailItems = this.ListView_EpochDetail.Items;

            detailItems.Clear();


            //add an entry for each species of the selected epoch
            for (int i = 0; i < info.PopulationSize; i++)
            {
                detailItems.Add($"#{i} ({info.Population[i].CachedScore})");
            }
        }

        //A different species has been selected
        private void EpochDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListView_EpochDetail.SelectedIndex >= 0) //check if no species is selected
            {
                //raise event
                onSpeciesSelected?.Invoke(this, epochs[ListView_EpochOverview.SelectedIndex].Population[ListView_EpochDetail.SelectedIndex]);
            }
        }

        //simulate the next epoch
        private async void NextEpoch_Click(object sender, RoutedEventArgs e)
        {
            onNextEpochClick?.Invoke(this, EventArgs.Empty);

            var epochTimerId = PseudoPerformanceTester.From();

            await Task.Factory.StartNew(() => geneticAlgorithm.DoEpoch());

            StorePopulation(epochStartSpecies, PseudoPerformanceTester.To(epochTimerId));
            
        }
    }
}
