using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.NeuralNetworks.FeedForward;
using NineMensMorris.GameLogic.Util;

namespace NineMensMorris.GameLogic.Players.AI
{
    /// <summary>
    /// Controls the flying phase of an AI
    /// </summary>
    public class FlyingController : ANeuralNetworkActionController<Move>
    {
        //all the moves that the controller can do... used for mapping
        private Move[] possibleMoves;

        public FlyingController(INetwork network, IPlayer player, Move[] possibleMoves) : base(network, player)
        {
            this.possibleMoves = possibleMoves;
        }

        public override RatedObject<Move> Map(Point[] inputs, in int index, in double value)
        {
            return new RatedObject<Move>(possibleMoves[index], value);
        }
    }
}
