using System;

namespace MazeGenAndPathFinding.Models.MazeGeneration
{
    public abstract class MazeGenerationAlgorithmBase
    {
        #region Properties

        public string Name { get; protected set; }

        public Maze Maze { get; private set; }

        #endregion

        #region Fields

        protected readonly Random Random = new Random();

        protected bool SuppressCellsChangedEvent;

        #endregion

        #region Methods

        public void SetMaze(Maze maze)
        {
            Maze = maze;
            Reset();
        }

        public abstract void Reset();

        public abstract void GenerateMaze();

        public abstract void Step();

        protected Cell GetRandomCell()
        {
            var x = Random.Next(0, Maze.Width);
            var y = Random.Next(0, Maze.Height);
            return Maze.Cells[x, y];
        }

        protected void RaiseCellsChangedEvent()
        {
            if (!SuppressCellsChangedEvent)
            {
                Maze.OnCellsChanged();
            }
        }

        #endregion
    }
}
