using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Represents a human player
    /// </summary>
    public class HumanPlayer : IPlayer
    {
        public int ID { get; private set; }

        private Game game; //reference to the host game

        private bool isActive = false;

        private bool manSelected = false;
        private Position lastClickPosition;

        public event EventHandler<Position> onManSelected;
        public event EventHandler<Position> onManDeselected;

        public void Init(Game game, int id)
        {
            this.game = game;
            this.ID = id;
        }

        public void BeginTurn(Game game)
        {
            isActive = true;

            manSelected = false;
        }

        public void EndTurn(Game game)
        {
            isActive = false;
        }

        /// <summary>
        /// Simulate a click on a point of the board
        /// </summary>
        /// <param name="currentActiveId"> The id of the player that was active when the click happened </param>
        // Introduced currentActiveId because playerA simulates his clicks, switches turns and then playerB also simulates the same click and erroneuosly thinks it is active!
        public void ClickPoint(int currentActiveId, Position position)
        {
            if(!isActive || currentActiveId != this.ID) //check if the player is active
            {
                return;
            }

            if(game.HasKillPending(this)) //the player has a pending kill
            {
                game.Kill(new Kill(this, position));
            }
            else
            {
                switch (game.CheckPhase(this))
                {
                    case Phase.Placing:
                        game.Place(new Placement(this, position)); //place down a man
                        break;
                    case Phase.Moving:
                    case Phase.Flying:
                        if (manSelected && game.GetOwnerId(position) != ID) //check if there is already a man selected and the newly selected man isn't one of our own
                        {
                            game.Move(new Move(this, lastClickPosition, position)); //move a man
                        }
                        else if(game.GetOwnerId(position) == this.ID) //make sure the first clicked man is one of ours
                        {
                            if (manSelected != false)
                            {
                                onManDeselected?.Invoke(this, lastClickPosition);
                            }

                            lastClickPosition = position;
                            manSelected = true;

                            //onManSelected event
                            onManSelected?.Invoke(this, lastClickPosition);
                        }
                        break;
                    default:
                        break;
                }
            
            }
        }
    }
}
