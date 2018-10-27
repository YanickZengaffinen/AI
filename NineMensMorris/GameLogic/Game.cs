using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    public class Game
    {
        private const int menBudget = 9; //How many men can the players each place
        private const int startFlying = 3; //When should the player be allowed to fly his men?
        private const int gameLost = 2; //At how many men is the player considered dead

        /// <summary>
        /// The id of points who do not belong to either one of the players
        /// </summary>
        public const int HostId = -1;

        /// <summary>
        /// The id of player x
        /// </summary>
        public const int PlayerAId = 0, PlayerBId = 1;

        //Both the players... should be private so no one can get their identities (at least not through the game class)
        private Dictionary<int, PlayerGameStatus> players = new Dictionary<int, PlayerGameStatus>(2);

        private IPlayer activePlayer; //The player whos turn it currently is
        private IPlayer inactivePlayer; //The player whos turn it currently isn't

        private int killsPending = 0; //How many kills are there pending for the current active player

        private Board board; //Representing the structure of a nine men's morris board

        //Events
        /// <summary>
        /// Raised whenever a player places a man
        /// </summary>
        public event EventHandler<ManPlacedEventArgs> onPlaced;
            
        /// <summary>
        /// Raised whenever a player moves a man
        /// </summary>
        public event EventHandler<ManMovedEventArgs> onMoved;

        /// <summary>
        /// Raised whenever a player kills a man
        /// </summary>
        public event EventHandler<ManKilledEventArgs> onKilled;

        /// <summary>
        /// Raised whenever the game is finished... takes the status of the winner as a parameter
        /// </summary>
        public event EventHandler<PlayerGameStatus> onGameFinished;

        public Game(IPlayer playerA, IPlayer playerB)
        {
            board = new Board();

            this.players.Add(PlayerAId, new PlayerGameStatus(playerA));
            this.players.Add(PlayerBId, new PlayerGameStatus(playerB));

            playerA.Init(this, PlayerAId);
            playerB.Init(this, PlayerBId);

            activePlayer = playerA;
            inactivePlayer = playerB;
            playerA.BeginTurn(this);
        }

        /// <summary>
        /// Try to place a men on a position for a player
        /// </summary>
        /// <returns> True if valid move </returns>
        public bool Place(IPlayer player, Position position)
        {
            if(player == activePlayer && CheckPhase(player) == Phase.Placing) //check rights
            {
                if(board[position] == HostId) //check if empty
                {
                    board[position] = player.ID;

                    players[player.ID].PlaceMan();

                    //invoke event
                    onPlaced?.Invoke(this, new ManPlacedEventArgs(player, position));

                    if(!CheckForMill(player, position)) //the placed man hasn't generated a new mill
                    {
                        SwitchTurn(); //End turn
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Try to move a men from a position to 
        /// </summary>
        /// <returns> True if valid move </returns>
        public bool Move(IPlayer player, Position from, Position to)
        {
            if(player == activePlayer)
            {
                //check if the move is valid
                if (board[from] == player.ID && //does the field belong to the player
                    board[to] == HostId && //check if the target field is empty
                    ((CheckPhase(player) == Phase.Moving && board.AreAdjacent(from, to)) || CheckPhase(player) == Phase.Flying)) //could the player possibly move from this position to the target
                {
                    board[from] = HostId;
                    board[to] = player.ID;

                    //invoke event
                    onMoved?.Invoke(this, new ManMovedEventArgs(player, from, to));

                    if (!CheckForMill(player, to)) //no mill generates due to this move
                    {
                        //the turn is completed --> switch
                        SwitchTurn();
                    }
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
            killsPending = board.GetPoint(position).GetCompleteLineCount(player.ID);

            return HasKillPending(player);
        }

        /// <summary>
        /// Try to kill a man at a certain position
        /// </summary>
        public bool Kill(IPlayer player, Position position)
        {
            if(player == activePlayer)
            {
                if(board[position] == inactivePlayer.ID) //check if the target point belongs to the enemy
                {
                    //check if the man that should be killed isn't part of a mill
                    if(board.GetPoint(position).GetCompleteLineCount(board[position]) > 0) //the man is part of a mill
                    {
                        var freePoints = board.GetFreePoints(inactivePlayer.ID);
                        if (freePoints.Count != 0 && !freePoints.Contains(board.GetPoint(position))) //if there are points that are not in a mill but the selected one isn't one of them
                        {
                            return false; //the man cannot be killed
                        }
                    }

                    board[position] = HostId;

                    players[inactivePlayer.ID].KillMan();

                    //invoke event
                    onKilled?.Invoke(this, new ManKilledEventArgs(player, position));

                    killsPending--;

                    if(IsDead(inactivePlayer))
                    {
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

        private void SwitchTurn()
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
            activePlayer.BeginTurn(this);
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
            return board[position];
        }
    }
}
