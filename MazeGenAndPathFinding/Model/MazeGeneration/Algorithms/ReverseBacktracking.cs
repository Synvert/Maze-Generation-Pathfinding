using MazeGenAndPathFinding.Model.DataModels;

namespace MazeGenAndPathFinding.Model.MazeGeneration.Algorithms
{
    public class ReverseBacktracking : IMazeGenerationAlgorithm
    {
        #region Properties

        public string Name { get; }
        public Maze Maze { get; private set; }

        #endregion

        #region Constructor

        public ReverseBacktracking()
        {
            Name = "Reverse Backtracking";
        }

        #endregion

        #region Methods

        public void Initialize(int height, int width)
        {
            Maze = new Maze(height, width);
        }

        #endregion
    }
}
