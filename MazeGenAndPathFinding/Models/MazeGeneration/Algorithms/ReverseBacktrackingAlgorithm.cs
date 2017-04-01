﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace MazeGenAndPathFinding.Models.MazeGeneration.Algorithms
{
    public class ReverseBacktrackingAlgorithm : MazeGenerationAlgorithmBase
    {
        #region Fields

        private readonly Stack<Cell> _currentChain = new Stack<Cell>();
        private readonly HashSet<Cell> _visitedCells = new HashSet<Cell>();
        private Cell _currentCell;
        private bool _isGenerated;

        #endregion

        #region Constructor

        public ReverseBacktrackingAlgorithm()
        {
            Name = "Reverse Backtracking";
        }

        #endregion

        #region Methods
        
        public override void Reset()
        {
            _currentChain.Clear();
            _visitedCells.Clear();

            Maze.ResetCellColors(Colors.LightGray);
            SetCurrentCell(GetRandomCell());

            Maze.ResetAllInteriorWalls(true);
            RaiseCellsChangedEvent();

            _isGenerated = false;
        }

        public override void GenerateMaze()
        {
            SuppressCellsChangedEvent = true;

            if (_isGenerated)
            {
                Reset();
            }
            while (!_isGenerated)
            {
                Step();
            }

            SuppressCellsChangedEvent = false;

            Maze.ResetCellColors(Colors.White);

            RaiseCellsChangedEvent();
        }

        public override void Step()
        {
            _visitedCells.Add(_currentCell);
            var neighboringCells = _currentCell.NeighboringCells
                .Where(x => !_visitedCells.Contains(x.Value))
                .ToList();
            if (neighboringCells.Any())
            {
                var randomNeighbor = neighboringCells.ElementAt(Random.Next(0, neighboringCells.Count));
                _currentCell.BreakWall(randomNeighbor.Key);
                _currentChain.Push(_currentCell);
                _currentCell.Background = Colors.LightBlue;
                SetCurrentCell(randomNeighbor.Value);
            }
            else
            {
                if (_currentChain.Any())
                {
                    _currentCell.Background = Colors.White;
                    SetCurrentCell(_currentChain.Pop());
                }
                else
                {
                    _isGenerated = true;
                    Maze.ResetCellColors(Colors.White);
                }
            }
            RaiseCellsChangedEvent();
        }

        private void SetCurrentCell(Cell cell)
        {
            _currentCell = cell;
            _currentCell.Background = Colors.LightCoral;
        }

        #endregion
    }
}
