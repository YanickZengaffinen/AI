using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Represents a line (=mill) in a nine men's morris game
    /// </summary>
    public class Line
    {
        /// <summary>
        /// The points that are part of this line
        /// </summary>
        public Point[] Points { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public Line(params Point[] points)
        {
            this.Points = points;
        }

        /// <summary>
        /// Checks if all the points of a line have the given owner
        /// </summary>
        public bool IsComplete(int id)
        {
            return Points.All(x => x.OwnerId == id);
        }

    }
}
