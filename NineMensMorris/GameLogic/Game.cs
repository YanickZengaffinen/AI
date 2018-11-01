using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    public class Game : IGame
    {
        /// <summary>
        /// The id of points who do not belong to either one of the players
        /// </summary>
        public const int HostId = 0;

        /// <summary>
        /// The id of player x
        /// </summary>
        public const int PlayerAId = -1, PlayerBId = 1;

        /// <summary>
        /// The board of the game
        /// </summary>
        public Board Board { get; private set; }

        /// <summary>
        /// Has one of the players less men on the board than the <see cref="gameLost"/>
        /// </summary>
        public bool IsFinished { get; protected set; }

        /// <summary>
        /// Has the game been aborted
        /// </summary>
        public bool IsAborted { get; protected set; }

        protected const int menBudget = 9; //How many men can the players each place
        protected const int startFlying = 3; //When should the player be allowed to fly his men?
        protected const int gameLost = 2; //At how many men is the player considered dead

        //Both the players... should be private so no one can get their identities (at least not through the game class)
        protected Dictionary<int, PlayerGameStatus> players = new Dictionary<int, PlayerGameStatus>(2);

        protected IPlayer activePlayer; //The player whos turn it currently is
        protected IPlayer inactivePlayer; //The player whos turn it currently isn't

        protected int killsPending = 0; //How many kills are there pending for the current active player

        //Events
        /// <summary>
        /// Raised whenever a player places a man
        /// </summary>
        public event EventHandler<Placement> onPlaced;
            
        /// <summary>
        /// Raised whenever a player moves a man
        /// </summary>
        public event EventHandler<Move> onMoved;

        /// <summary>
        /// Raised whenever a player kills a man
        /// </summary>
        public event EventHandler<Kill> onKilled;

        /// <summary>
        /// Raised whenever the game is finished... takes the status of the winner as a parameter
        /// </summary>
        public event EventHandler<PlayerGameStatus> onGameFinished;

        public Game(IPlayer playerA, IPlayer playerB)
        {
            Board = new Board(HostId);

            this.players.Add(PlayerAId, new PlayerGameStatus(playerA));
            this.players.Add(PlayerBId, new PlayerGameStatus(playerB));

            playerA.Init(this, PlayerAId);
            playerB.Init(this, PlayerBId);

            activePlayer = playerA;
            inactivePlayer = playerB;
        }

        public bool NextTurn()
        {
            if (!IsFinished && !IsAborted)
            {
                activePlayer.BeginTurn(this);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Automatically starts the next turn once a player has finished
        /// </summary>
        public void DoGameLoop()
        {
            while (NextTurn()) ; //cannot be done recursively as there might be >10000 turns which causes a StackOverflowException
        }

        /// <summary>
        /// Try to place a men on a position for a player
        /// </summary>
        /// <returns> True if valid move </returns>
        public virtual bool Place(Placement placement)
        {
            var player = placement.Player;
            var position = placement.Target;

            if(player == activePlayer && CheckPhase(player) == Phase.Placing) //check rights
            {
                if(Board[position] == HostId) //check if empty
                {
                    Board[position] = player.ID;

                    players[player.ID].PlaceMan();

                    //invoke event
                    onPlaced?.Invoke(this, placement);

                    if(!CheckForMill(player, position)) //the placed man hasn't generated a new mill
                    {
                        SwitchTurn(); //End turn
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try to move a men from a position to 
        /// </summary>
        /// <returns> True if valid move </returns>
        public virtual bool Move(Move move)
        {
            var player = move.Player;
            var from = move.Start;
            var to = move.Destination;

            if(player == activePlayer)
            {
                //check if the move is valid
                if (Board[from] == player.ID && //does the field belong to the player
                    Board[to] == HostId && //check if the target field is empty
                    ((CheckPhase(player) == Phase.Moving && Board.AreAdjacent(from, to)) || CheckPhase(player) == Phase.Flying)) //could the player possibly move from this position to the target
                {
                    Board[from] = HostId;
                    Board[to] = player.ID;

                    //invoke event
                    onMoved?.Invoke(this, move);

                    if (!CheckForMill(player, to)) //no mill generates due to this move
                    {
                        //the turn is completed --> switch
                        SwitchTurn();
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check whether or not the changed status of a point has generated a mill
        /// and initiate the killing logic (killPending = true)
        /// </summary>
        private bool CheckForMill(IPlayer player, Position position)
        {
            killsPending = Board.GetPoint(position).GetCompleteLineCount(player.ID);

            return HasKillPending(player);
        }

        /// <summary>
        /// Try to kill a man at a certain position
        /// </summary>
        public virtual bool Kill(Kill kill)
        {
            var player = kill.Player;
            var position = kill.Target;

            if(player == activePlayer)
            {
                if(Board[position] == inactivePlayer.ID) //check if the target point belongs to the enemy
                {
                    //check if the man that should be killed isn't part of a mill
                    if(Board.GetPoint(position).GetCompleteLineCount(Board[position]) > 0) //the man is part of a mill
                    {
                        var freePoints = Board.GetFreePoints(inactivePlayer.ID);
                        if (freePoints.Count != 0 && !freePoints.Contains(Board.GetPoint(position))) //if there are points that are not in a mill but the selected one isn't one of them
                        {
                            return false; //the man cannot be killed
                        }
                    }

                    Board[position] = HostId;

                    players[inactivePlayer.ID].KillMan();

                    //invoke event
                    onKilled?.Invoke(this, kill);

                    killsPending--;

                    if(IsDead(inactivePlayer))
                    {
                        IsFinished = true;

                        onGameFinished?.Invoke(this, players[player.ID]);
                    }

                    //finally: switch turns
                    if(!HasKillPending(player))
                    {
                        SwitchTurn();
                    }

                    return true;
                }
            }

            return false;
        }

        protected void SwitchTurn()
        {
            if(HasKillPending(activePlayer))
            {
                throw new Exception("Cannot switch turns if there are pending kills!");
            }

            //switch the current and non-current player
            var player = activePlayer;
            activePlayer = inactivePlayer;
            inactivePlayer = player;

            //send notifications
            inactivePlayer.EndTurn(this);

        }

        /// <summary>
        /// Check if a player has a kill pending
        /// </summary>
        public bool HasKillPending(IPlayer player)
        {
            return player == activePlayer && killsPending > 0;
        }

        /// <summary>
        /// Get the current phase a player is in
        /// </summary>
        public Phase CheckPhase(IPlayer player)
        {
            var playerStatus = players[player.ID];

            if(playerStatus.MenPlaced >= menBudget)
            {
                if(playerStatus.MenAlive <= startFlying)
                {
                    return Phase.Flying;
                }

                return Phase.Moving;
            }

            return Phase.Placing;
        }

        /// <summary>
        /// Check if a player is dead
        /// </summary>
        public bool IsDead(IPlayer player)
        {
            var playerStats = players[player.ID];
            return playerStats.MenPlaced >= menBudget && playerStats.MenAlive <= gameLost;
        }

        /// <summary>
        /// Get the player that is currently active
        /// </summary>
        public IPlayer GetActivePlayer()
        {
            return activePlayer;
        }

        /// <summary>
        /// Get the owner id of a position
        /// </summary>
        public int GetOwnerId(Position position)
        {
            return Board[position];
        }

        /// <summary>
        /// Get all points of the board
        /// </summary>
        public Point[] GetPoints()
        {
            return Board.AllPoints;
        }

        /// <summary>
        /// Get the status of a player
        /// </summary>
        public PlayerGameStatus GetStatus(IPlayer player)
        {
            return players[player.ID];
        }
    }
}
