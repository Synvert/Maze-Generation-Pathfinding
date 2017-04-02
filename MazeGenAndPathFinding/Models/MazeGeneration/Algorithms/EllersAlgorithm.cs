using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MazeGenAndPathFinding.Models.MazeGeneration.Algorithms
{
    public class EllersAlgorithm : MazeGenerationAlgorithmBase
    {
        #region Fields

        private readonly Dictionary<Cell, CellSet> _cellSetLookup = new Dictionary<Cell, CellSet>();
        private readonly Dictionary<int, int> _numberOfCellsInSetInCurrentRowLookup = new Dictionary<int, int>();
        private readonly List<CellSet> _verticallyMergedSets = new List<CellSet>();
        private Action _currentStep;
        private int _currentX;
        private int _currentY;

        #endregion

        #region Constructor

        public EllersAlgorithm()
        {
            Name = "Eller's";
        }

        #endregion

        #region Methods

        public override void Reset()
        {
            base.Reset();
            _currentStep = MergeHorizontally;
            _currentX = 0;
            _currentY = 0;

            _cellSetLookup.Clear();
            // Initialize the cells of the first row to each exist in their own set.
            for (var x = 0; x < Maze.Width; x++)
            {
                _cellSetLookup[Maze.Cells[x, 0]] = new CellSet(Maze.Cells[x, 0]);
            }

            IsInitialized = true;
        }

        public override void Step()
        {
            _currentStep();
            Maze.OnCellsChanged();
        }

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            var initialStep = _currentStep;
            do
            {
                Step();
                await Task.Delay(25, cancellationToken);
            } while (initialStep == _currentStep && !IsComplete);
        }

        public override async Task RunToEndAsync(CancellationToken cancellationToken)
        {
            await base.RunToEndAsync(cancellationToken);
        }

        private void MergeHorizontally()
        {
            var leftCell = Maze.Cells[_currentX, _currentY];
            var rightCell = Maze.Cells[_currentX + 1, _currentY];
            var leftSet = _cellSetLookup[leftCell];
            var rightSet = _cellSetLookup[rightCell];

            if (leftSet.Id != rightSet.Id)
            {
                if (Random.Next(2) == 0)
                {
                    // Merge sets
                    leftSet.Merge(rightSet, _cellSetLookup);

                    leftCell.BreakWall(Direction.East);
                    rightCell.Background = Colors.White;
                }
            }

            // Update set counter
            AddSetToCurrentRowTracker(leftSet);

            leftCell.Background = Colors.White;
            _currentX++;
            if (_currentX == Maze.Width - 1)
            {
                rightCell.Background = Colors.White;
                _currentStep = MergeVertically;

                // Update set counter
                AddSetToCurrentRowTracker(rightSet);
            }
        }

        private void MergeVertically()
        {
            var topCell = Maze.Cells[_currentX, _currentY];
            var topSet = _cellSetLookup[topCell];

            var mustMerge = _numberOfCellsInSetInCurrentRowLookup[topSet.Id] == 1
                            && !_verticallyMergedSets.Contains(topSet);

            if (mustMerge || Random.Next(2) == 0)
            {
                _verticallyMergedSets.Add(topSet);

                var bottomCell = Maze.Cells[_currentX, _currentY + 1];

                // Merge sets
                topSet.Cells.Add(bottomCell);
                _cellSetLookup[bottomCell] = topSet;

                topCell.BreakWall(Direction.South);
                bottomCell.Background = Colors.White;
            }
            _numberOfCellsInSetInCurrentRowLookup[topSet.Id]--;
            if (_currentX == 0)
            {
                _verticallyMergedSets.Clear();
                _currentStep = PrepareRow;
            }
            else
            {
                _currentX--;
            }
        }

        private void PrepareRow()
        {
            var cell = Maze.Cells[_currentX, _currentY + 1];
            if (!_cellSetLookup.ContainsKey(cell))
            {
                _cellSetLookup.Add(cell, new CellSet(cell));
                cell.Background = Colors.White;
            }
            if (_currentX != Maze.Width - 1)
            {
                _currentX++;
            }
            else
            {
                _currentX = 0;
                _currentY++;
                if (_currentY != Maze.Height - 1)
                {
                    _currentStep = MergeHorizontally;
                }
                else
                {
                    _currentStep = LastRow;
                }
            }
        }

        private void LastRow()
        {
            var cell = Maze.Cells[_currentX, _currentY];
            var nextCell = Maze.Cells[_currentX + 1, _currentY];

            if (_cellSetLookup[cell].Id != _cellSetLookup[nextCell].Id)
            {
                // Merge sets
                _cellSetLookup[cell].Merge(_cellSetLookup[nextCell], _cellSetLookup);

                cell.BreakWall(Direction.East);
                nextCell.Background = Colors.White;
            }

            cell.Background = Colors.White;
            _currentX++;
            if (_currentX == Maze.Width - 1)
            {
                nextCell.Background = Colors.White;
                IsComplete = true;
            }
        }

        private void AddSetToCurrentRowTracker(CellSet set)
        {
            if (!_numberOfCellsInSetInCurrentRowLookup.ContainsKey(set.Id))
            {
                _numberOfCellsInSetInCurrentRowLookup.Add(set.Id, 1);
            }
            else
            {
                _numberOfCellsInSetInCurrentRowLookup[set.Id]++;
            }
        }
        
        #endregion

        #region NestedTypes

        private class CellSet
        {
            #region Properties

            public List<Cell> Cells { get; }

            public int Id { get; }

            #endregion

            #region Fields

            private static int _idPool;

            #endregion

            #region Constructors
            
            public CellSet(Cell cell)
            {
                Cells = new List<Cell> { cell };
                Id = _idPool++;
            }

            #endregion

            #region Methods

            public void Merge(CellSet other, IDictionary<Cell, CellSet> cellSetLookup)
            {
                Cells.AddRange(other.Cells);
                foreach (var cell in other.Cells)
                {
                    cellSetLookup[cell] = this;
                }
            }

            #endregion
        }

        #endregion
    }
}
