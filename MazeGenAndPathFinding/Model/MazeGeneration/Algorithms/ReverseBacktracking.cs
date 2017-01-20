using System.Collections.Generic;
using System.Linq;
using MazeGenAndPathFinding.Model.DataModels;

namespace MazeGenAndPathFinding.Model.MazeGeneration.Algorithms
{
    public class ReverseBacktracking : MazeGenerationAlgoithmBase
    {
        #region Fields

        private readonly Stack<Cell> _currentChain = new Stack<Cell>();
        private readonly HashSet<Cell> _visitedCells = new HashSet<Cell>();
        private Cell _currentCell;
        private bool _isGenerated;

        #endregion

        #region Constructor

        public ReverseBacktracking()
        {
            Name = "Reverse Backtracking";
        }

        #endregion

        #region Methods

        public override void Initialize(int height, int width)
        {
            base.Initialize(height, width);
            _currentCell = GetRandomCell();
        }

        public override void Reset()
        {
            _currentChain.Clear();
            _visitedCells.Clear();
            _currentCell = GetRandomCell();

            Maze.ResetAllInteriorWalls(false);
            Maze.OnCellsChanged();

            _isGenerated = false;
        }

        public override void GenerateMaze()
        {
            if (_isGenerated)
            {
                Reset();
            }

            while (!Step())
            {
            }

            Maze.OnCellsChanged();
            _isGenerated = true;
        }

        public override bool Step()
        {
            _visitedCells.Add(_currentCell);
            var neighboringCells = Maze.GetNeighboringCells(_currentCell)
                .Where(x => !_visitedCells.Contains(x.Value))
                .ToList();
            if (neighboringCells.Any())
            {
                var randomNeighbor = neighboringCells.ElementAt(Random.Next(0, neighboringCells.Count));
                _currentCell.BreakWall(randomNeighbor.Key);
                _currentChain.Push(_currentCell);

                Maze.OnCellsChanged();

                _currentCell = randomNeighbor.Value;
            }
            else
            {
                if (_currentChain.Any())
                {
                    _currentCell = _currentChain.Pop();
                }
                else
                {
                    _isGenerated = true;
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
