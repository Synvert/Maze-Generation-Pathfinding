using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MazeGenAndPathFinding.Models.MazeGeneration
{
    public abstract class MazeGenerationAlgorithmBase
    {
        #region Properties

        public string Name { get; protected set; }

        public bool IsRunAvailable { get; protected set; }

        public bool IsComplete { get; protected set; }

        protected bool IsInitialized { get; set; }
        
        protected Maze Maze { get; private set; }

        protected Random Random { get; } = new Random();

        #endregion

        #region Methods

        public void SetMaze(Maze maze)
        {
            Maze = maze;
            Reset();
            IsComplete = false;
        }

        public virtual void Reset()
        {
            Maze.ResetCellColors(Colors.LightGray);
            Maze.ResetAllInteriorWalls(true);
            Maze.OnCellsChanged();
            IsComplete = false;
            IsInitialized = false;
        }

        public abstract void Step();

        public virtual Task RunAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public abstract Task RunToEndAsync(CancellationToken cancellationToken);
        
        protected Cell GetRandomCell()
        {
            var x = Random.Next(0, Maze.Width);
            var y = Random.Next(0, Maze.Height);
            return Maze.Cells[x, y];
        }
        
        #endregion
    }
}
