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

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new instance of <see cref="Cell"/>.
        /// </summary>
        public Cell()
        {
            Walls = new Dictionary<Direction, Wall>
            {
                { Direction.North, new Wall() },
                { Direction.East, new Wall() },
                { Direction.South, new Wall() },
                { Direction.West, new Wall() },
            };
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
