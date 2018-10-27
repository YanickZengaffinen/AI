using AI.NeuralNetworks.FeedForward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic.Players.AI
{
    public class PlacerNN
    {
        private INetwork network;
        
        public PlacerNN(INetwork network)
        {
            this.network = network;
        }

        /// <summary>
        /// Evaluates the best positions at which a man should be placed
        /// </summary>
        public IList<Tuple<Point, double>> GetRankedPoints(Point[] points)
        {
            var rVal = new List<Tuple<Point, double>>(points.Length);

            //feed the network with the current board and calculate the best position
            network.Calculate(points.Select(x => (double)x.OwnerId).ToArray());

            var outNeurons = network.OutputLayer.Neurons;

            for(int i = 0; i < outNeurons.Count; i++)
            {
                rVal.Add(Tuple.Create(points[i], outNeurons[i].Value));
            }

            rVal = rVal.OrderBy(x => x.Item2).ToList(); //order the list by the neurons output value

            return rVal;
        }
    }
}
