using MazeGenAndPathFinding.Models;
using MazeGenAndPathFinding.Models.MazeGeneration;

namespace MazeGenAndPathFinding.ViewModels
{
    public class MazeGenerationAlgorithimViewModel : AlgorithmViewModelBase
    {
        #region Properties

        public MazeGenerationAlgorithmBase MazeGenerationAlgorithm => Algorithm as MazeGenerationAlgorithmBase;

        #endregion
        #region Constructor

        public MazeGenerationAlgorithimViewModel(MazeGenerationAlgorithmBase mazeGenerationAlgorithm)
            : base(mazeGenerationAlgorithm)
        {
        }

        #endregion

        #region Methods

        public void SetMaze(Maze maze)
        {
            CancellationTokenSource = null;
            MazeGenerationAlgorithm.SetMaze(maze);
        }

        #endregion
    }
}
