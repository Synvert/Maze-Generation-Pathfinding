using System.Collections.Generic;
using System.Linq;
using MazeGenAndPathFinding.Models;
using MazeGenAndPathFinding.Models.MazeGeneration.Algorithms;
using Prism.Commands;
using Prism.Mvvm;

namespace MazeGenAndPathFinding.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Properties

        public IList<MazeGenerationAlgorithimViewModel> MazeGenerationAlgorithms { get; }

        public IList<PathFindingAlgorithmViewModel> PathFindingAlgorithms { get; }

        public MazeGenerationAlgorithimViewModel SelectedMazeGenerationAlgorithm
        {
            get { return _selectedMazeGenerationAlgorithm; }
            set
            {
                if (_selectedMazeGenerationAlgorithm != value)
                {
                    _selectedMazeGenerationAlgorithm?.Cancel();
                    _selectedMazeGenerationAlgorithm = value;
                    _selectedMazeGenerationAlgorithm.SetMaze(Maze);
                    OnPropertyChanged(nameof(SelectedMazeGenerationAlgorithm));
                }
            }
        }
        private MazeGenerationAlgorithimViewModel _selectedMazeGenerationAlgorithm;

        public PathFindingAlgorithmViewModel SelectedPathFindingAlgorithm
        {
            get { return _selectedPathFindingAlgorithm; }
            set { SetProperty(ref _selectedPathFindingAlgorithm, value); }
        }
        private PathFindingAlgorithmViewModel _selectedPathFindingAlgorithm;

        public Maze Maze
        {
            get { return _maze; }
            protected set { SetProperty(ref _maze, value); }
        }
        private Maze _maze;

        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }
        private int _width = 10;

        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }
        private int _height = 10;

        #endregion

        #region Commands

        #region	ApplyGridSettingsCommand

        public DelegateCommand ApplyGridSettingsCommand { get; }

        private void OnApplyGridSettingsCommandExecuted()
        {
            var maze = new Maze(Width, Height);
            SelectedMazeGenerationAlgorithm.SetMaze(maze);
            Maze = maze;
        }

        #endregion

        #endregion

        #region Constructor

        public MainViewModel()
        {
            ApplyGridSettingsCommand = new DelegateCommand(OnApplyGridSettingsCommandExecuted);

            MazeGenerationAlgorithms = new List<MazeGenerationAlgorithimViewModel>
            {
                new MazeGenerationAlgorithimViewModel(new EllersAlgorithm()),
                new MazeGenerationAlgorithimViewModel(new PrimsAlgorithm()),
                new MazeGenerationAlgorithimViewModel(new ReverseBacktrackingAlgorithm()),
            };
            PathFindingAlgorithms = new List<PathFindingAlgorithmViewModel>
            {
                new PathFindingAlgorithmViewModel(new Models.PathFinding.Algorithms.ReverseBacktrackingAlgorithm())
            };

            Maze = new Maze(Width, Height);
            SelectedMazeGenerationAlgorithm = MazeGenerationAlgorithms.First();
            SelectedPathFindingAlgorithm = PathFindingAlgorithms.First();
        }

        #endregion

        #region Methods
        
        #endregion
    }
}
