using AI.NeuralNetworks;
using AI.NeuralNetworks.ActivationFunctions;
using AI.NeuralNetworks.FeedForward.Learning;
using AI.NeuralNetworks.GeneticAlgorithms;
using AI_UI.NeuralNetworks.GeneticAlgorithms;
using NineMensMorris.GameLogic;
using NineMensMorris.GameLogic.Players.AI;
using NineMensMorris.GeneticAlgorithms;
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

using AI.Util;
using PerformanceTesting.Pseudo;

namespace NineMensMorris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class NineMensMorrisWindow : Window
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

        private Game game;

        private GameRecorder recorder; //records the game

        //Event that gets called whenever a board point is being clicked... 
        //int represents the active player at the moment of the click
        private event EventHandler<Tuple<int, Position>> boardPointClicked;    

        public NineMensMorrisWindow()
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

            //NewHumanVsHumanGame(null, null);
        }

        /// <summary>
        /// Starts a displayed game between two players
        /// </summary>
        public void StartDisplayedGame(IPlayer a, IPlayer b)
        {
            CreateDisplayedNewGame(a, b);

            game.AutoStartNextTurn = true;
            game.NextTurn();
        }

        /// <summary>
        /// Starts a human vs ai game
        /// </summary>
        public void PlayAgainstAI(AIPlayer ai)
        {
            StartDisplayedGame(CreateHumanPlayer(), ai);
        }

        /// <summary>
        /// Starts a game human vs genetic AI
        /// </summary>
        public void PlayAgainstGeneticAI(GeneticGameAI ai)
        {
            var aiPlayer = new AIPlayer();
            CreateDisplayedNewGame(CreateHumanPlayer(), aiPlayer);

            ai.OverrideEmptyAIPlayer(aiPlayer, game);

            game.AutoStartNextTurn = true;
            game.NextTurn();
        }

        //opens a new trainer window
        private SpeciesDetailWindow training_speciesDetailWindow;
        private void OpenTrainer(object sender, RoutedEventArgs e)
        {
            var mockBoard = new Board();

            var starter = GenerateStarter(
                (uint)mockBoard.AllPoints.Length,
                (uint)mockBoard.GetAllMovesAdjacents(null).Length,
                (uint)mockBoard.GetAllMoves(null).Length);

            var geneticAlgorithm = new GeneticAlgorithm(50, starter, 0.25f);

            var trainer = new GeneticAlgorithmTrainerWindow(geneticAlgorithm);

            //detail window of species
            training_speciesDetailWindow = new SpeciesDetailWindow();
            training_speciesDetailWindow.Show();
            training_speciesDetailWindow.onPlayClicked += (s, info) => {
                PlayAgainstGeneticAI(info.AI);
            };

            //subscribe to trainer events
            trainer.onSpeciesSelected += (s, species) => {
                training_speciesDetailWindow.SetInfo((species as GeneticGameAI).CachedSpeciesInfo);
            };

            trainer.onNextEpochClick += (object s, EventArgs ea) => {
                foreach(var species in geneticAlgorithm.Population)
                {
                    (species as GeneticGameAI).SetEnemy((geneticAlgorithm.Population[0] as GeneticGameAI));
                }
            };

            trainer.Show();
        }

        /// <summary>
        /// Generates a random starter AI
        /// </summary>
        private ISpecies GenerateStarter(in uint allPointsCnt, in uint allMovesAdjacentCnt, in uint allMovesCnt)
        {
            var placementNetwork = NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                SigmoidFunction.Instance, allPointsCnt, allPointsCnt, 10);

            var moveNetwork = NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                SigmoidFunction.Instance, allPointsCnt, allMovesAdjacentCnt, 100, 100);

            var killNetwork = NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                SigmoidFunction.Instance, allPointsCnt, allPointsCnt, 100, 100);

            var flyNetwork = NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                SigmoidFunction.Instance, allPointsCnt, allMovesCnt, 100, 100);

            return new GeneticGameAI(placementNetwork, moveNetwork, killNetwork, flyNetwork);
        }

        //Game where both players are humans
        private void NewHumanVsHumanGame(object sender, RoutedEventArgs e)
        {
            //clear the eventhandler
            boardPointClicked = null;

            StartDisplayedGame(CreateHumanPlayer(), CreateHumanPlayer());
        }

        //Game where a human plays against an AI
        private void NewHumanVsAIGame(object sender, RoutedEventArgs e)
        {
            var ai = new AIPlayer();

            CreateDisplayedNewGame(CreateHumanPlayer(), ai);

            //TODO: enable to load saved networks
            //setup the controllers for the AI
            var placementCtrl = new PlacementController(NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                SigmoidFunction.Instance, (uint)allPoints.Length, (uint)allPoints.Length, 10), 
                ai); 

            var allMovesAdjacent = game.Board.GetAllMovesAdjacents(ai);
            var moveCtrl = new MoveController(NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                SigmoidFunction.Instance, (uint)allPoints.Length, (uint)allMovesAdjacent.Length, 100, 100), 
                ai, allMovesAdjacent);

            var killCtrl = new KillController(NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                SigmoidFunction.Instance, (uint)allPoints.Length, (uint)allPoints.Length, 100, 100),
                ai);

            var allMoves = game.Board.GetAllMoves(ai);
            var flyingCtrl = new FlyingController(NetworkGenerator.GenerateFullyConnectedFeedForwardNetwork(
                SigmoidFunction.Instance, (uint)allPoints.Length, (uint)allMoves.Length, 100, 100),
                ai, allMoves);

            ai.Init(placementCtrl, moveCtrl, killCtrl, flyingCtrl);


            PlayAgainstAI(ai);
        }

        //Creates a displayed game
        private void CreateDisplayedNewGame(IPlayer a, IPlayer b)
        {
            //create the game
            game = new Game(a, b);

            recorder = new GameRecorder(game);

            //subscribe to game events
            DisplayGame(game);

            //Set the color of all buttons to neutral
            foreach (var button in allPoints)
            {
                button.Background = colorBrushMap[Game.HostId];
            }
        }

        //Subscribes to the game events in order for it to be rendered correctly
        private void DisplayGame(Game game)
        {
            game.onPlaced += Game_OnPlaced;
            game.onMoved += Game_OnMoved;
            game.onKilled += Game_OnKilled;
            game.onGameFinished += Game_Finished;
        }

        //Creates a human player and subscribes to the click event
        private HumanPlayer CreateHumanPlayer()
        {
            var rVal = new HumanPlayer();

            boardPointClicked += (object s, Tuple<int, Position> args) => rVal.ClickPoint(args.Item1, args.Item2);

            //UI
            rVal.onManSelected += (object s, Position position) => GetButton(position).Background = selectedColorBrush; //highlight the button
            rVal.onManDeselected += (object s, Position position) => GetButton(position).Background = colorBrushMap[game.Board.GetPoint(position).OwnerId]; //reset the button to its normal value

            return rVal;
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

        //Whenever the game has been finished
        private void Game_Finished(object sender, PlayerGameStatus status)
        {
            if(MessageBox.Show($"player {status.Player.ID} won with {status.MenAlive} men still alive.", "Do you want to watch the replay?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var replayWindow = new NineMensMorrisReplayWindow();
                replayWindow.SetRecorder(recorder);
                replayWindow.Show();
            }
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
