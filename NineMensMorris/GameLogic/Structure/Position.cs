using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// Represents a position of a men in the game. E.g. A7
    /// </summary>
    public struct Position
    {
        public int Row { get; }
        public Letter Column { get; }
        public int ColumnInt => (int)Column;

        /// <summary>
        /// C'tor
        /// </summary>
        public Position(int row, Letter column)
        {
            this.Row = row;
            this.Column = column;
        }

        /// <summary>
        /// C'tor which takes the column as a integer
        /// </summary>
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = (Letter)column;
        }

        /// <summary>
        /// C'tor from a string (e.g. A7)
        /// </summary>
        public Position(string pos)
        {
            this.Row = int.Parse(new String(pos.Where(Char.IsNumber).ToArray()));
            this.Column = (Letter)Enum.Parse(typeof(Letter), new String(pos.Where(Char.IsLetter).ToArray()).ToUpper());
        }

        //Overrides of GetHashCode and Equals to allow usage as Dictionary key
        public override int GetHashCode()
        {
            return Row * 31 + ColumnInt;
        }

        public override bool Equals(object obj)
        {
            var other = (Position)obj;

            return other.Row == Row && other.Column == Column;
        }

        /// <summary>
        /// Get the position as a string of format LetterNumber (A0)
        /// </summary>
        public override string ToString()
        {
            return Column.ToString() + Row;
        }
    }

    public enum Letter
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G
    }
}
