using System.Collections.Generic;
using MazeGenAndPathFinding.Extensions;

namespace MazeGenAndPathFinding.Model.DataModels
{
    public class Cell
    {
        #region Properties

        /// <summary>
        /// Gets whether or not a wall is present in a given <see cref="Direction"/>.
        /// </summary>
        public Dictionary<Direction, bool> Walls { get; }

        /// <summary>
        /// Gets the cells in a given direction from this cell.
        /// </summary>
        public Dictionary<Direction, Cell> NeighboringCells { get; set; }

        /// <summary>
        /// Gets the X coordinate of this <see cref="Cell"/>.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the Y coordinate of this <see cref="Cell"/>.
        /// </summary>
        public int Y { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new instance of <see cref="Cell"/>.
        /// </summary>
        /// <param name="x">The X coordinate of this <see cref="Cell"/>.</param>
        /// <param name="y">the Y coordinate of this <see cref="Cell"/>.</param>
        public Cell(int x, int y)
        {
            Walls = new Dictionary<Direction, bool>
            {
                {Direction.North, true},
                {Direction.East, true},
                {Direction.South, true},
                {Direction.West, true}
            };
            X = x;
            Y = y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Breaks a the <see cref="Wall"/> in <see cref="Walls"/> that corresponds to <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction">The <see cref="Direction"/> to break the wall in.</param>
        public void BreakWall(Direction direction)
        {
            Walls[direction] = false;
            if (NeighboringCells.ContainsKey(direction))
            {
                NeighboringCells[direction].Walls[direction.GetOpposite()] = false;
            }
        }

        #endregion
    }
}
