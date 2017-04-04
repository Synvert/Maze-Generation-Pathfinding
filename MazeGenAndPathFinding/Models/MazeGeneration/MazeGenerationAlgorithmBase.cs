using System.Windows.Media;

namespace MazeGenAndPathFinding.Models.MazeGeneration
{
    public abstract class MazeGenerationAlgorithmBase : AlgorithmBase
    {
        #region Properties
        
        protected bool IsInitialized { get; set; }
        
        protected Maze Maze { get; private set; }
        
        #endregion

        #region Methods

        public void SetMaze(Maze maze)
        {
            Maze = maze;
            Reset();
            IsComplete = false;
        }

        public override void Reset()
        {
            Maze.ResetCellColors(Colors.LightGray);
            Maze.ResetAllInteriorWalls(true);
            Maze.OnCellsChanged();
            IsComplete = false;
            IsInitialized = false;
        }
        
        protected Cell GetRandomCell()
        {
            var x = Random.Next(0, Maze.Width);
            var y = Random.Next(0, Maze.Height);
            return Maze.Cells[x, y];
        }
        
        #endregion
    }
}
