using System.Collections.Generic;

namespace MazeGenAndPathFinding.Model.DataModels
{
    public class Cell
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="Wall"/>s of the <see cref="Cell"/> for each <see cref="Direction"/>. 
        /// </summary>
        public Dictionary<Direction, Wall> Walls { get; }

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
            Walls = new Dictionary<Direction, Wall>
            {
                { Direction.North, new Wall() },
                { Direction.East, new Wall() },
                { Direction.South, new Wall() },
                { Direction.West, new Wall() },
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
            Walls[direction].IsBroken = true;
        }

        #endregion
    }
}
