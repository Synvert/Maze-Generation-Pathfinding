using System;
using System.Threading;
using System.Threading.Tasks;

namespace MazeGenAndPathFinding.Models
{
    public abstract class AlgorithmBase
    {
        #region Properties

        public string Name { get; protected set; }

        public bool IsComplete { get; protected set; }

        public bool IsRunAvailable { get; protected set; } = true;

        protected Random Random { get; } = new Random();

        #endregion

        #region Fields

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public virtual void Reset()
        {
        }

        public abstract void Step();

        public virtual Task RunAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public virtual async Task RunToEndAsync(CancellationToken cancellationToken)
        {
            while (!IsComplete)
            {
                Step();
                await Task.Delay(16, cancellationToken);
            }
        }

        #endregion
    }
}
