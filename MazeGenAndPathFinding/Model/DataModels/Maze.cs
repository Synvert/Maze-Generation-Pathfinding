using System;

namespace MazeGenAndPathFinding.Model.DataModels
{
    /// <summary>
    /// Represents a grid of <see cref="Cell"/>s.
    /// </summary>
    public class Maze
    {
        #region Properties

        /// <summary>
        /// Gets the width of the <see cref="Width"/>.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of the <see cref="Maze"/>.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the 2D array of <see cref="Cell"/>s that make up the maze. They are stored in the order X,Y, or Width,Height.
        /// </summary>
        public Cell[,] Cells { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new instance of <see cref="Maze"/>.
        /// </summary>
        /// <param name="width">The width of the <see cref="Maze"/>. Must be greater than 1.</param>
        /// <param name="height">The height of the <see cref="Maze"/>. Must be greater than 1.</param>
        public Maze(int width, int height)
        {
            if (width <= 1)
                throw new ArgumentException("Must be an integer greater than 1.", nameof(width));
            if (height <= 1)
                throw new ArgumentException("Must be an integer greater than 1.", nameof(height));

            Width = width;
            Height = height;

            //Create Default Cells
            Cells = new Cell[Width, Height];
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    Cells[x, y] = new Cell();
                }
            }

            MergeCommonCellWalls();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Iterates over <see cref="Cells"/>, merging walls shared between cells so they share the same reference.
        /// </summary>
        /// <remarks>
        /// This is useful for when <see cref="Cell.BreakWall"/> is called, the change is reflected in the neighboring cell.
        /// </remarks>
        private void MergeCommonCellWalls()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    // Both x and y must be either even or odd together.
                    // This avoids unnecessary assignments.
                    var xMod2 = x%2;
                    var yMod2 = y%2;
                    if (!((xMod2 == 0 && yMod2 == 0)
                          || (xMod2 != 0 && yMod2 != 0)))
                    {
                        continue;
                    }

                    for (Direction direction = 0; (int) direction < 4; direction++)
                    {
                        var cellInDirection = TryGetCellInDirection(x, y, direction);
                        if (cellInDirection == null)
                        {
                            continue;
                        }
                        cellInDirection.Walls[GetOppositeDirection(direction)] = Cells[x, y].Walls[direction];
                    }
                }
            }
        }

        /// <summary>
        /// Finds the opposite <see cref="Direction"/> of <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction">The <see cref="Direction"/> to find the opposite of.</param>
        /// <returns>The direction opposite of <paramref name="direction"/>.</returns>
        private static Direction GetOppositeDirection(Direction direction)
        {
            return (Direction) (((int) direction + 2)%4);
        }

        /// <summary>
        /// Attempts to return a <see cref="Cell"/> located at coordinates <paramref name="x"/>,<paramref name="y"/>.
        /// </summary>
        /// <param name="x">The X coordinate to start from.</param>
        /// <param name="y">The Y coordinate to start from.</param>
        /// <returns>A cell, if one is found, otherwise, null.</returns>
        private Cell TryGetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                return null;
            }

            return Cells[x, y];
        }

        /// <summary>
        /// Attempts to return a neighboring <see cref="Cell"/> to the <see cref="Cell"/> located at coordinates 
        /// <paramref name="x"/>,<paramref name="y"/> in a given <paramref name="direction"/>.
        /// </summary>
        /// <param name="x">The X coordinate to start from.</param>
        /// <param name="y">The Y coordinate to start from.</param>
        /// <param name="direction">The direction to look check for a cell from the given coordinates.</param>
        /// <returns>A cell, if one is found, otherwise, null.</returns>
        private Cell TryGetCellInDirection(int x, int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    y--;
                    break;
                case Direction.East:
                    x++;
                    break;
                case Direction.South:
                    y++;
                    break;
                case Direction.West:
                    x--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            return TryGetCell(x, y);
        }

        #endregion
    }
}
