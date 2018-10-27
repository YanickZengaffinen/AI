using AI.NeuralNetworks.FeedForward;
using NineMensMorris.GameLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic.Players.AI
{
    /// <summary>
    /// Controls the movement phase of an AI
    /// </summary>
    public class MoveController : ANeuralNetworkActionController<Move>
    {
        //All the moves that are possible on a board. Used for mapping
        private Move[] possibleMoves;

        public MoveController(INetwork network, IPlayer player, Move[] possibleMoves) : base(network, player) {
            this.possibleMoves = possibleMoves;
        }

        public override RatedObject<Move> Map(Point[] inputs, in int index, in double value)
        {
            return new RatedObject<Move>(possibleMoves[index], value);
        }
    }

}
