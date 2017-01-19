using MazeGenAndPathFinding.Model.MazeGeneration;
using MazeGenAndPathFinding.Model.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenAndPathFinding.Model
{
    public class AlgorithmManager : IAlgorithmManager
    {
        #region Constructor

        public AlgorithmManager()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            MazeGenerationAlgorithms = types.Where(t => t.GetInterfaces().Contains(typeof(IMazeGenerationAlgorithm)) && t.GetConstructor(Type.EmptyTypes) != null)
                                            .Select(t => Activator.CreateInstance(t) as IMazeGenerationAlgorithm)
                                            .ToList();
            PathFindingAlgorithms = types.Where(t => t.GetInterfaces().Contains(typeof(IPathFindingAlgorithm)) && t.GetConstructor(Type.EmptyTypes) != null)
                                           .Select(t => Activator.CreateInstance(t) as IPathFindingAlgorithm)
                                           .ToList();
        }

        #endregion

        #region Properties

        public List<IMazeGenerationAlgorithm> MazeGenerationAlgorithms { get; private set; }
        public List<IPathFindingAlgorithm> PathFindingAlgorithms { get; private set; }

        #endregion
    }
}
