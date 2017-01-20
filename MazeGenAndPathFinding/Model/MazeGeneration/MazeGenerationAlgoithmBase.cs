using System;
using MazeGenAndPathFinding.Model.DataModels;
using Prism.Mvvm;

namespace MazeGenAndPathFinding.Model.MazeGeneration
{
    public abstract class MazeGenerationAlgoithmBase
    {
        #region Properties

        public string Name { get; protected set; }

        public Maze Maze { get; protected set; }

        #endregion

        #region Fields

        protected readonly Random Random = new Random();

        #endregion

        #region Methods

        public virtual void Initialize(int height, int width)
        {
            Maze = new Maze(height, width);
        }

        public abstract void ResetMaze();

        public abstract void GenerateMaze();

        protected Cell GetRandomCell()
        {
            var x = Random.Next(0, Maze.Width);
            var y = Random.Next(0, Maze.Height);
            return Maze.Cells[x, y];
        }

        #endregion
    }
}
