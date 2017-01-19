using MazeGenAndPathFinding.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenAndPathFinding.Model.MazeGeneration
{
    public interface IMazeGenerationAlgorithm
    {
        #region Properties

        string Name { get; }
        string Description { get; }

        #endregion

        #region Methods

        void Initialize(int height, int width);
        Maze StepThrough(out bool isComplete);
        Maze GenerateMaze();

        #endregion
    }
}
