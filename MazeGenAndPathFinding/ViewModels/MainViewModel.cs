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

        public IList<MazeGenerationAlgorithmBase> MazeGenerationAlgorithms { get; }

        public IList<IPathFindingAlgorithm> PathFindingAlgorithms { get; }

        public MazeGenerationAlgorithmBase SelectedMazeGenerationAlgorithm
        {
            get { return _selectedMazeGenerationAlgorithm; }
            set
            {
                if (SetProperty(ref _selectedMazeGenerationAlgorithm, value))
                {
                    value.Initialize(Width, Height);
                }
            }
        }
        private MazeGenerationAlgorithmBase _selectedMazeGenerationAlgorithm;

        public IPathFindingAlgorithm SelectedPathFindingAlgorithm
        {
            get { return _selectedPathFindingAlgorithm; }
            set { SetProperty(ref _selectedPathFindingAlgorithm, value); }
        }
        private IPathFindingAlgorithm _selectedPathFindingAlgorithm;

        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }
        private int _width = 25;

        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }
        private int _height = 25;

        #endregion

        #region Commands

        #region	ResetMazeCommand

        public DelegateCommand ResetMazeCommand { get; }

        private void OnResetMazeCommandExecuted()
        {
            SelectedMazeGenerationAlgorithm.Initialize(Width, Height);
            SelectedMazeGenerationAlgorithm.Reset();
        }

        #endregion

        #region	StepMazeCommand

        public DelegateCommand StepMazeCommand { get; }

        private void OnStepMazeCommandExecuted()
        {
            _selectedMazeGenerationAlgorithm.Step();
        }

        #endregion

        #region	GenerateMazeCommand

        public DelegateCommand GenerateMazeCommand { get; }

        private void OnGenerateMazeCommandExecuted()
        {
            SelectedMazeGenerationAlgorithm.Initialize(Width, Height);
            SelectedMazeGenerationAlgorithm.GenerateMaze();
        }

        #endregion

        #endregion

        #region Constructor

        public MainViewModel()
        {
            ResetMazeCommand = new DelegateCommand(OnResetMazeCommandExecuted);
            StepMazeCommand = new DelegateCommand(OnStepMazeCommandExecuted);
            GenerateMazeCommand = new DelegateCommand(OnGenerateMazeCommandExecuted);

            MazeGenerationAlgorithms = new List<MazeGenerationAlgorithmBase>
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
