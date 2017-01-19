using MazeGenAndPathFinding.Model.MazeGeneration;
using MazeGenAndPathFinding.Model.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenAndPathFinding.Model
{
    public interface IAlgorithmManager
    {
        #region Properties

        List<IMazeGenerationAlgorithm> MazeGenerationAlgorithms { get; }
        List<IPathFindingAlgorithm> PathFindingAlgorithms { get; }

        #endregion
    }
}
