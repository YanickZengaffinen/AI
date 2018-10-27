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
    /// Selects a man the AI should kill
    /// </summary>
    public class KillController : ANeuralNetworkActionController<Kill>
    {
        public KillController(INetwork network, IPlayer player) : base(network, player) { }

        public override RatedObject<Kill> Map(Point[] inputs, in int index, in double value)
        {
            return new RatedObject<Kill>(new Kill(Player, inputs[index].Position), value);    
        }
    }
}
