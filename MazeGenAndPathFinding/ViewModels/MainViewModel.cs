using System.Collections.Generic;
using System.Linq;
using MazeGenAndPathFinding.Model.MazeGeneration;
using MazeGenAndPathFinding.Model.MazeGeneration.Algorithms;
using MazeGenAndPathFinding.Model.PathFinding;
using MazeGenAndPathFinding.Model.PathFinding.Algorithms;
using Prism.Commands;
using Prism.Mvvm;

namespace MazeGenAndPathFinding.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Properties

        public IList<MazeGenerationAlgoithmBase> MazeGenerationAlgorithms { get; }

        public IList<IPathFindingAlgorithm> PathFindingAlgorithms { get; }

        public MazeGenerationAlgoithmBase SelectedMazeGenerationAlgorithm
        {
            get { return _selectedMazeGenerationAlgorithm; }
            set
            {
                if (SetProperty(ref _selectedMazeGenerationAlgorithm, value))
                {
                    value.Initialize(DefaultWidth, DefaultHeight);
                }
            }
        }
        private MazeGenerationAlgoithmBase _selectedMazeGenerationAlgorithm;

        public IPathFindingAlgorithm SelectedPathFindingAlgorithm
        {
            get { return _selectedPathFindingAlgorithm; }
            set { SetProperty(ref _selectedPathFindingAlgorithm, value); }
        }
        private IPathFindingAlgorithm _selectedPathFindingAlgorithm;

        #endregion

        #region Commands

        #region	ResetMazeCommand

        public DelegateCommand ResetMazeCommand { get; }

        private void OnResetMazeCommandExecuted()
        {
            SelectedMazeGenerationAlgorithm.ResetMaze();
        }

        #endregion

        #region	GenerateMazeCommand

        public DelegateCommand GenerateMazeCommand { get; }

        private void OnGenerateMazeCommandExecuted()
        {
            SelectedMazeGenerationAlgorithm.GenerateMaze();
        }

        #endregion

        #endregion

        #region Fields

        private const int DefaultWidth = 10;
        private const int DefaultHeight = 10;

        #endregion

        #region Constructor

        public MainViewModel()
        {
            ResetMazeCommand = new DelegateCommand(OnResetMazeCommandExecuted);
            GenerateMazeCommand = new DelegateCommand(OnGenerateMazeCommandExecuted);

            MazeGenerationAlgorithms = new List<MazeGenerationAlgoithmBase>
            {
                new ReverseBacktracking()
            };
            PathFindingAlgorithms = new List<IPathFindingAlgorithm>
            {
                new AStar()
            };

            SelectedMazeGenerationAlgorithm = MazeGenerationAlgorithms.First();
            SelectedPathFindingAlgorithm = PathFindingAlgorithms.First();
        }

        #endregion
    }
}
