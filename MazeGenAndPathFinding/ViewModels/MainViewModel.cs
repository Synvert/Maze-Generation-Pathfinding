using System.Collections.Generic;
using System.Linq;
using MazeGenAndPathFinding.Model.MazeGeneration;
using MazeGenAndPathFinding.Model.MazeGeneration.Algorithms;
using MazeGenAndPathFinding.Model.PathFinding;
using MazeGenAndPathFinding.Model.PathFinding.Algorithms;
using Prism.Mvvm;

namespace MazeGenAndPathFinding.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Properties

        public IList<IMazeGenerationAlgorithm> MazeGenerationAlgorithms { get; }

        public IList<IPathFindingAlgorithm> PathFindingAlgorithms { get; }

        public IMazeGenerationAlgorithm SelectedMazeGenerationAlgorithm
        {
            get { return _selectedMazeGenerationAlgorithm; }
            set { SetProperty(ref _selectedMazeGenerationAlgorithm, value); }
        }
        private IMazeGenerationAlgorithm _selectedMazeGenerationAlgorithm;

        public IPathFindingAlgorithm SelectedPathFindingAlgorithm
        {
            get { return _selectedPathFindingAlgorithm; }
            set { SetProperty(ref _selectedPathFindingAlgorithm, value); }
        }
        private IPathFindingAlgorithm _selectedPathFindingAlgorithm;

        #endregion

        #region Constructor

        public MainViewModel()
        {
            MazeGenerationAlgorithms = new List<IMazeGenerationAlgorithm>
            {
                new ReverseBacktracking()
            };
            PathFindingAlgorithms = new List<IPathFindingAlgorithm>
            {
                new AStar()
            };

            _selectedMazeGenerationAlgorithm = MazeGenerationAlgorithms.First();
            _selectedPathFindingAlgorithm = PathFindingAlgorithms.First();
        }

        #endregion
    }
}
