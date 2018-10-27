using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Representing one point in a field of a nine men's morris game.
    /// Uses fluent API
    /// </summary>
    public class Point
    {
        /// <summary>
        /// This points position on the field
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// The owner of this point
        /// </summary>
        internal int OwnerId { get; set; }

        /// <summary>
        /// The adjacents/neighbours of this point on a field
        /// </summary>
        public IList<Point> Adjacents { get; private set; } = new List<Point>();

        /// <summary>
        /// A list of all lines (=mills) that include this point
        /// </summary>
        public IList<Line> Lines { get; private set; } = new List<Line>();

        /// <summary>
        /// C'tor
        /// </summary>
        public Point(Position position)
        {
            this.Position = position;
        }

        /// <summary>
        /// C'tor that internally generates a position
        /// </summary>
        public Point(int row, Letter column)
        {
            this.Position = new Position(row, column);
        }

        /// <summary>
        /// Connects points
        /// </summary>
        public Point Link(params Point[] targets)
        {
            foreach(var point in targets)
            {
                point.Adjacents.Add(this);
                Adjacents.Add(point);
            }

            return this;
        }

        /// <summary>
        /// Adds a reference to a line containing this point
        /// </summary>
        public Point AddLine(Line line)
        {
            Lines.Add(line);

            return this;
        }

        /// <summary>
        /// Check how many of the lines this point is a part of are complete (placing a man might finish 2 mills)
        /// </summary>
        public int GetCompleteLineCount(int ownerId)
        {
            if (OwnerId == ownerId)
            {
                return Lines.Where(x => x.IsComplete(ownerId)).Count();
            }

            return 0;
        }

        /// <summary>
        /// Check if there is at least one complete line (=mill)
        /// </summary>
        public bool HasCompleteLine(int ownerId)
        {
            return Lines.Any(x => x.IsComplete(ownerId));
        }
    }
}
