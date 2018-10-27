using NineMensMorris.GameLogic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //which player should have which color
        private Dictionary<int, SolidColorBrush> colorMap = new Dictionary<int, SolidColorBrush>()
        {
            { Game.HostId, new SolidColorBrush(Color.FromRgb(255, 255, 255))},
            { Game.PlayerAId, new SolidColorBrush(Color.FromRgb(255, 0, 0))},
            { Game.PlayerBId, new SolidColorBrush(Color.FromRgb(0, 0, 255))}
        };

        private Button[] allPoints;

        private Game game;

        //Event that gets called whenever a board point is being clicked... 
        //int represents the active player at the moment of the click
        private event EventHandler<Tuple<int, Position>> boardPointClicked;    

        public MainWindow()
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


            NewHumanVsHumanGame(null, null);
        }

        //Game where both players are humans
        private void NewHumanVsHumanGame(object sender, RoutedEventArgs e)
        {
            var humanA = new HumanPlayer();
            var humanB = new HumanPlayer();

            game = new Game(humanA, humanB);

            //subscribe to game events
            game.onPlaced += Game_OnPlaced;
            game.onMoved += Game_OnMoved;
            game.onKilled += Game_OnKilled;
            game.onGameFinished += Game_Finished;

            //subscribe to click events for players
            boardPointClicked = null;
            boardPointClicked += (object s, Tuple<int, Position> args) => humanA.ClickPoint(args.Item1, args.Item2);
            boardPointClicked += (object s, Tuple<int, Position> args) => humanB.ClickPoint(args.Item1, args.Item2);


            //Set the color of all buttons to neutral
            foreach (var button in allPoints)
            {
                button.Background = colorMap[Game.HostId];
            }
        }

        //Whenever a man has been placed
        private void Game_OnPlaced(object sender, ManPlacedEventArgs args)
        {
            var button = GetButton(args.Position);

            button.Background = colorMap[args.Owner.ID];
        }

        //Whenever a man has been moved
        private void Game_OnMoved(object sender, ManMovedEventArgs args)
        {
            var buttonFrom = GetButton(args.From);
            var buttonTo = GetButton(args.To);

            buttonFrom.Background = colorMap[Game.HostId];
            buttonTo.Background = colorMap[args.By.ID];
        }

        //Whenever a man has been killed
        private void Game_OnKilled(object sender, ManKilledEventArgs args)
        {
            var button = GetButton(args.Target);

            button.Background = colorMap[Game.HostId];//new SolidColorBrush(Color.FromRgb(0, 255, 0));//
        }

        //Whenever the game has been finished
        private void Game_Finished(object sender, PlayerGameStatus status)
        {
            MessageBox.Show($"player {status.Player.ID} won with {status.MenAlive} men still alive");
        }

        //Whenever the user clicks on a point of the board
        private void Board_Clicked(object sender, RoutedEventArgs e)
        {
            boardPointClicked?.Invoke(this, Tuple.Create(game.GetActivePlayer().ID, new Position((sender as Button).Name)));
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
