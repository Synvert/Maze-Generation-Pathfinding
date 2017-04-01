using MazeGenAndPathFinding.Models;
using MazeGenAndPathFinding.Models.MazeGeneration;
using Prism.Commands;
using Prism.Mvvm;

namespace MazeGenAndPathFinding.ViewModels.MazeGeneration
{
    public abstract class MazeGenerationAlgorithimViewModelBase : BindableBase
    {
        #region Properties

        public string Name { get; protected set; }

        #endregion
        
        #region Commands

        #region ResetCommand

        public DelegateCommand ResetCommand { get; }

        protected virtual void OnResetCommandExecuted()
        {
            Algorithm.Reset();
        }

        #endregion

        #region	StepCommand

        public DelegateCommand StepCommand { get; }

        protected virtual void OnStepCommandExecuted()
        {
            Algorithm.Step();
        }

        #endregion

        #region	GenerateCommand

        public DelegateCommand GenerateCommand { get; }

        protected virtual void OnGenerateCommandExecuted()
        {
            Algorithm.GenerateMaze();
        }

        #endregion

        #endregion

        #region Fields
        
        protected MazeGenerationAlgorithmBase Algorithm;
        
        #endregion

        #region Constructor

        protected MazeGenerationAlgorithimViewModelBase()
        {
            ResetCommand = new DelegateCommand(OnResetCommandExecuted);
            StepCommand = new DelegateCommand(OnStepCommandExecuted);
            GenerateCommand = new DelegateCommand(OnGenerateCommandExecuted);
        }

        #endregion

        #region Methods

        public void SetMaze(Maze maze)
        {
            Algorithm.SetMaze(maze);
        }

        #endregion
    }
}
