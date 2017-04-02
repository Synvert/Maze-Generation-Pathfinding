using System.Collections.Generic;
using System.Linq;
using MazeGenAndPathFinding.Models;
using MazeGenAndPathFinding.Models.MazeGeneration.Algorithms;
using MazeGenAndPathFinding.Models.PathFinding;
using MazeGenAndPathFinding.Models.PathFinding.Algorithms;
using MazeGenAndPathFinding.ViewModels.MazeGeneration;
using Prism.Commands;
using Prism.Mvvm;

namespace MazeGenAndPathFinding.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Properties

        public IList<MazeGenerationAlgorithimViewModel> MazeGenerationAlgorithms { get; }

        public IList<IPathFindingAlgorithm> PathFindingAlgorithms { get; }

        public MazeGenerationAlgorithimViewModel SelectedMazeGenerationAlgorithm
        {
            get { return _selectedMazeGenerationAlgorithm; }
            set
            {
                if (SetProperty(ref _selectedMazeGenerationAlgorithm, value))
                {
                    SelectedMazeGenerationAlgorithm.SetMaze(Maze);
                }
            }
        }
        private MazeGenerationAlgorithimViewModel _selectedMazeGenerationAlgorithm;

        public IPathFindingAlgorithm SelectedPathFindingAlgorithm
        {
            get { return _selectedPathFindingAlgorithm; }
            set { SetProperty(ref _selectedPathFindingAlgorithm, value); }
        }
        private IPathFindingAlgorithm _selectedPathFindingAlgorithm;

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
                new MazeGenerationAlgorithimViewModel(new ReverseBacktrackingAlgorithm()),
            };
            PathFindingAlgorithms = new List<IPathFindingAlgorithm>
            {
                new AStar()
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
