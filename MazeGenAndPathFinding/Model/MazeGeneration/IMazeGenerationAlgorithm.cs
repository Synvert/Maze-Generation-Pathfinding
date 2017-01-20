using MazeGenAndPathFinding.Model.DataModels;

namespace MazeGenAndPathFinding.Model.MazeGeneration
{
    public interface IMazeGenerationAlgorithm
    {
        #region Properties

        string Name { get; }

        Maze Maze { get; }

        #endregion

        #region Methods

        void Initialize(int height, int width);

        #endregion
    }
}
