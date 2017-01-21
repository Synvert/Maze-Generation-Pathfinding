using System;
using MazeGenAndPathFinding.Model.DataModels;
using Prism.Mvvm;

namespace MazeGenAndPathFinding.Model.MazeGeneration
{
    public abstract class MazeGenerationAlgorithmBase : BindableBase
    {
        #region Properties

        public string Name { get; protected set; }

        public Maze Maze
        {
            get { return _maze; }
            protected set { SetProperty(ref _maze, value); }
        }
        private Maze _maze;

        #endregion

        #region Fields

        protected readonly Random Random = new Random();

        protected bool SuppressCellsChangedEvent;

        #endregion

        #region Methods

        public virtual void Initialize(int height, int width)
        {
            Maze = new Maze(height, width);
        }

        public abstract void Reset();

        public abstract void GenerateMaze();

        public abstract bool Step();

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
