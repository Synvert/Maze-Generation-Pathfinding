using System;
using System.Threading;
using System.Threading.Tasks;
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

        private CancellationTokenSource CancellationTokenSource
        {
            get { return _cancellationTokenSource; }
            set
            {
                if (_cancellationTokenSource != value)
                {
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource?.Dispose();
                    _cancellationTokenSource = value;
                    StepCommand.RaiseCanExecuteChanged();
                    RunCommand.RaiseCanExecuteChanged();
                    RunToEndCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private CancellationTokenSource _cancellationTokenSource;

        #endregion

        #region Commands

        #region ResetCommand

        public DelegateCommand ResetCommand { get; }

        protected virtual void OnResetCommandExecuted()
        {
            CancellationTokenSource = null;
            Algorithm.Reset();
            StepCommand.RaiseCanExecuteChanged();
            RunCommand.RaiseCanExecuteChanged();
            RunToEndCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region	StepCommand

        public DelegateCommand StepCommand { get; }

        protected virtual void OnStepCommandExecuted()
        {
            Algorithm.Step();
        }

        private bool CanStepCommandExecute()
        {
            return _cancellationTokenSource == null && !Algorithm.IsComplete;
        }

        #endregion

        #region	RunCommand

        public DelegateCommand RunCommand { get; }

        private void OnRunCommandExecuted()
        {
            RunAsyncFunc(Algorithm.RunAsync);
        }

        private bool CanRunCommandExecute()
        {
            return _cancellationTokenSource == null && !Algorithm.IsComplete;
        }

        #endregion

        #region	RunToEndCommand

        public DelegateCommand RunToEndCommand { get; }

        protected virtual void OnRunToEndCommandExecuted()
        {
            RunAsyncFunc(Algorithm.RunToEndAsync);
        }

        private bool CanRunToEndCommandExecute()
        {
            return _cancellationTokenSource == null && !Algorithm.IsComplete;
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
            StepCommand = new DelegateCommand(OnStepCommandExecuted, CanStepCommandExecute);
            RunCommand = new DelegateCommand(OnRunCommandExecuted, CanRunCommandExecute);
            RunToEndCommand = new DelegateCommand(OnRunToEndCommandExecuted, CanRunToEndCommandExecute);
        }

        #endregion

        #region Methods

        public void SetMaze(Maze maze)
        {
            CancellationTokenSource = null;
            Algorithm.SetMaze(maze);
        }

        private void RunAsyncFunc(Func<CancellationToken, Task> func)
        {
            CancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                await func(CancellationTokenSource.Token);
                CancellationTokenSource = null;
            });
        }

        #endregion
    }
}
