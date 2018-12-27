using AI.NeuralNetworks.GeneticAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NineMensMorris.GeneticAlgorithms
{
    /// <summary>
    /// Interaction logic for SpeciesDetailWindow.xaml
    /// </summary>
    public partial class SpeciesDetailWindow : Window
    {
        /// <summary>
        /// Called whenever the play button gets clicked
        /// </summary>
        public event EventHandler<SpeciesInfo> onPlayClicked;

        private SpeciesInfo currentInfo;

        public SpeciesDetailWindow()
        {
            InitializeComponent();
        }

        public void SetInfo(SpeciesInfo info)
        {
            this.currentInfo = info;

            Button_Play.IsEnabled = true;

            Label_Win.Content = info.Aborted ? "Aborted" : info.Win ? "Yes" : "No";
            Label_Alive.Content = info.MenAlive;
            Label_Kills.Content = $"{info.MenKilled} / {info.InvalidKills}";
            Label_Moves.Content = $"{info.Moves} / {info.InvalidMoves}";
            Label_Flights.Content = $"{info.Flights} / {info.InvalidFlights}";
            Label_InvalidPlacements.Content = info.InvalidPlacements;

        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            onPlayClicked?.Invoke(this, currentInfo);
        }
    }
}
