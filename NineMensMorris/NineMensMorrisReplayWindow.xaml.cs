using AI.NeuralNetworks;
using AI.NeuralNetworks.ActivationFunctions;
using NineMensMorris.GameLogic;
using NineMensMorris.GameLogic.Players.AI;
using NineMensMorris.Replay;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NineMensMorris
{
    /// <summary>
    /// Interaction logic for NineMensMorrisReplayWindow.xaml
    /// </summary>
    public partial class NineMensMorrisReplayWindow : Window
    {
        //which player should have which color
        private Dictionary<int, SolidColorBrush> colorBrushMap = new Dictionary<int, SolidColorBrush>()
        {
            { Game.HostId, new SolidColorBrush(Color.FromRgb(255, 255, 255))},
            { Game.PlayerAId, new SolidColorBrush(Color.FromRgb(255, 0, 0))},
            { Game.PlayerBId, new SolidColorBrush(Color.FromRgb(0, 0, 255))}
        };

        private SolidColorBrush selectedColorBrush = new SolidColorBrush(Color.FromRgb(255, 255, 0));

        private Button[] allPoints;

        private GameReplay replay;

        public NineMensMorrisReplayWindow()
        {
            InitializeComponent();

            allPoints = new Button[] {
                a6, d6, g6,
                b5, d5, f5,
                c4, d4, e4,
                a3, b3, c3, e3, f3, g3,
                c2, d2, e2,
                b1, d1, f1,
                a0, d0, g0
            };

        }

        public void SetRecorder(GameRecorder recorder)
        {
            replay = new GameReplay(recorder);

            DisplayGame(replay.Game);

            //Set the color of all buttons to neutral
            foreach (var button in allPoints)
            {
                button.Background = colorBrushMap[Game.HostId];
            }
        }

        //Executes the next step of the replay
        private void NextStep(object sender, RoutedEventArgs args)
        {
            replay.Next();
        }

        //Subscribes to the game events in order for it to be rendered correctly
        private void DisplayGame(IGame game)
        {
            game.onPlaced += Game_OnPlaced;
            game.onMoved += Game_OnMoved;
            game.onKilled += Game_OnKilled;
        }

        //Whenever a man has been placed
        private void Game_OnPlaced(object sender, Placement placement)
        {
            var button = GetButton(placement.Target);

            button.Background = colorBrushMap[placement.Player.ID];
        }

        //Whenever a man has been moved
        private void Game_OnMoved(object sender, Move move)
        {
            var buttonFrom = GetButton(move.Start);
            var buttonTo = GetButton(move.Destination);

            buttonFrom.Background = colorBrushMap[Game.HostId];
            buttonTo.Background = colorBrushMap[move.Player.ID];
        }

        //Whenever a man has been killed
        private void Game_OnKilled(object sender, Kill kill)
        {
            var button = GetButton(kill.Target);

            button.Background = colorBrushMap[Game.HostId];
        }

        /// <summary>
        /// Get the button that represents a position
        /// </summary>
        private Button GetButton(Position position)
        {
            return allPoints.First(x => x.Name.Equals(position.ToString().ToLower()));
        }
    }
}
