using AI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Represents the board of a nine men's morris game
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Points of the outter ring
        /// </summary>
        public readonly Point
            A6 = new Point(6, Letter.A),
            D6 = new Point(6, Letter.D),
            G6 = new Point(6, Letter.G),
            A3 = new Point(3, Letter.A),
            G3 = new Point(3, Letter.G),
            A0 = new Point(0, Letter.A),
            D0 = new Point(0, Letter.D),
            G0 = new Point(0, Letter.G);

        /// <summary>
        /// Points of the middle ring
        /// </summary>
        public readonly Point
            B5 = new Point(5, Letter.B),
            D5 = new Point(5, Letter.D),
            F5 = new Point(5, Letter.F),
            B3 = new Point(3, Letter.B),
            F3 = new Point(3, Letter.F),
            B1 = new Point(1, Letter.B),
            D1 = new Point(1, Letter.D),
            F1 = new Point(1, Letter.F);

        /// <summary>
        /// Points of the inner ring
        /// </summary>
        public readonly Point
            C4 = new Point(4, Letter.C),
            D4 = new Point(4, Letter.D),
            E4 = new Point(4, Letter.E),
            C3 = new Point(3, Letter.C),
            E3 = new Point(3, Letter.E),
            C2 = new Point(2, Letter.C),
            D2 = new Point(2, Letter.D),
            E2 = new Point(2, Letter.E);

        /// <summary>
        /// An array of all points of the board
        /// </summary>
        public Point[] AllPoints { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public Board(int defaultOwnerId = -1)
        {
            AllPoints = new Point[]{
                A6, D6, G6,
                B5, D5, F5,
                C4, D4, E4,
                A3, B3, C3, E3, F3, G3,
                C2, D2, E2,
                B1, D1, F1,
                A0, D0, G0
            };

            foreach (var point in AllPoints)
            {
                point.OwnerId = defaultOwnerId;
            }

            ConnectPoints();
            SetupLines();
        }

        /// <summary>
        /// Indexer
        /// </summary>
        public int this[Position position] {
            get => GetPoint(position).OwnerId;
            set { GetPoint(position).OwnerId = value; }
        }

        /// <summary>
        /// Connects all the points of the board
        /// </summary>
        private void ConnectPoints()
        {
            //outtter ring
            D6.Link(A6, G6, D5);
            G3.Link(G6, F3, G0);
            D0.Link(D1, A0, G0);
            A3.Link(A6, A0, B3);

            //middle ring
            B3.Link(B5, B1, C3);
            D5.Link(B5, F5, D4);
            F3.Link(F5, E3, F1);
            D1.Link(B1, F1, D2);

            //inner ring
            D2.Link(C2, E2);
            C3.Link(C2, C4);
            D4.Link(C4, E4);
            E3.Link(E4, E2);
        }

        /// <summary>
        /// Sets up all the lines
        /// </summary>
        private void SetupLines()
        {
            //horizontal lines
            SetupLine(A6, D6, G6);
            SetupLine(B5, D5, F5);
            SetupLine(C4, D4, E4);
            SetupLine(A3, B3, C3);
            SetupLine(E3, F3, G3);
            SetupLine(C2, D2, E2);
            SetupLine(B1, D1, F1);
            SetupLine(A0, D0, G0);

            //vertical lines
            SetupLine(A6, A3, A0);
            SetupLine(B5, B3, B1);
            SetupLine(C4, C3, C2);
            SetupLine(D6, D5, D4);
            SetupLine(D2, D1, D0);
            SetupLine(E4, E3, E2);
            SetupLine(F5, F3, F1);
            SetupLine(G6, G3, G0);
        }

        /// <summary>
        /// Sets up a single line
        /// </summary>
        private void SetupLine(Point a, Point b, Point c)
        {
            var line = new Line(a, b, c);

            a.AddLine(line);
            b.AddLine(line);
            c.AddLine(line);
        }

        /// <summary>
        /// Check if two positions are adjacent on this board
        /// </summary>
        public bool AreAdjacent(Position a, Position b)
        {
            return GetPoint(a).Adjacents.Contains(GetPoint(b));
        }

        /// <summary>
        /// Gets a point for a position
        /// </summary>
        public Point GetPoint(Position position)
        {
            return AllPoints.First(x => x.Position.Equals(position));
        }

        /// <summary>
        /// Get all points that are currently not part of any line/mill
        /// </summary>
        public IList<Point> GetFreePoints(int ownerId)
        {
            return AllPoints.Where(x => x.OwnerId == ownerId && !x.HasCompleteLine(ownerId)).ToList();
        }
    }
}
