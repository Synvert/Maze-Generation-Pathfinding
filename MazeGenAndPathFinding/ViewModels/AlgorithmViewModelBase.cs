using System;
using System.Threading;
using System.Threading.Tasks;
using MazeGenAndPathFinding.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace MazeGenAndPathFinding.ViewModels
{
    public class AlgorithmViewModelBase : BindableBase
    {
        #region Properties

        public string Name => Algorithm.Name;

        protected AlgorithmBase Algorithm { get; }

        protected CancellationTokenSource CancellationTokenSource
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
            return _cancellationTokenSource == null && !Algorithm.IsComplete && Algorithm.IsRunAvailable;
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

        #region Constructor
        
        public AlgorithmViewModelBase(AlgorithmBase algorithm)
        {
            Algorithm = algorithm;

            ResetCommand = new DelegateCommand(OnResetCommandExecuted);
            StepCommand = new DelegateCommand(OnStepCommandExecuted, CanStepCommandExecute);
            RunCommand = new DelegateCommand(OnRunCommandExecuted, CanRunCommandExecute);
            RunToEndCommand = new DelegateCommand(OnRunToEndCommandExecuted, CanRunToEndCommandExecute);
        }

        #endregion

        #region Methods

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private void RunAsyncFunc(Func<CancellationToken, Task> func)
        {
            CancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                try
                {
                    await func(CancellationTokenSource.Token);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                CancellationTokenSource = null;
            });
        }

        #endregion
    }
}
