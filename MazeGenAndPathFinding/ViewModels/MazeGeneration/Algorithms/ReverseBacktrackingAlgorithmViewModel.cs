using MazeGenAndPathFinding.Models.MazeGeneration.Algorithms;

namespace MazeGenAndPathFinding.ViewModels.MazeGeneration.Algorithms
{
    public class ReverseBacktrackingAlgorithmViewModel : MazeGenerationAlgorithimViewModelBase
    {
        #region Fields
        
        #endregion

        #region Constructor

        public ReverseBacktrackingAlgorithmViewModel()
        {
            Name = "Reverse Backtracking";
            Algorithm = new ReverseBacktrackingAlgorithm();
        }

        #endregion

        #region Methods

        #endregion
    }
}
