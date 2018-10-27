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
    /// Controls the placement phase of an AI
    /// </summary>
    public class PlacementController : ANeuralNetworkActionController<Placement>
    {
        public PlacementController(INetwork network, IPlayer player) : base(network, player) { }

        public override RatedObject<Placement> Map(Point[] inputs, in int index, in double value)
        {
            return new RatedObject<Placement>(new Placement(Player, inputs[index].Position), value);
        }
    }
}
