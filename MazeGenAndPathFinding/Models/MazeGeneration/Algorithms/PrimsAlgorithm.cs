using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace MazeGenAndPathFinding.Models.MazeGeneration.Algorithms
{
    public class PrimsAlgorithm : MazeGenerationAlgorithmBase
    {
        #region Fields
        
        private readonly List<Cell> _frontierCells = new List<Cell>();
        private readonly HashSet<Cell> _incorporatedCells = new HashSet<Cell>();

        #endregion

        #region Constructor

        public PrimsAlgorithm()
        {
            Name = "Prim's";
            IsRunAvailable = false;
        }

        #endregion

        #region Methods

        public override void Reset()
        {
            base.Reset();
            _frontierCells.Clear();
            _incorporatedCells.Clear();
        }

        public override void Step()
        {
            if (!IsInitialized)
            {
                IncorporateCell(GetRandomCell());
                Maze.OnCellsChanged();
                IsInitialized = true;
                return;
            }

            // Choose a random "Frontier" cell.
            var randomIndex = Random.Next(0, _frontierCells.Count);
            var cell = _frontierCells[randomIndex];
            _frontierCells.RemoveAt(randomIndex);

            // Add the chosen cell to the incorporated cells.
            IncorporateCell(cell);

            // Get the potential connections to the incorporated cells from the chosen cell.
            var potentialConnections = cell.NeighboringCells.Where(x => _incorporatedCells.Contains(x.Value)).ToList();

            // Connect the chosen cell back to the incorporated cells.
            cell.BreakWall(potentialConnections[Random.Next(0, potentialConnections.Count)].Key);
            
            if (_frontierCells.Count == 0)
            {
                IsComplete = true;
            }
            Maze.OnCellsChanged();
        }

        private void IncorporateCell(Cell cell)
        {
            _incorporatedCells.Add(cell);
            cell.Background = Colors.White;

            // Add the valid neighboring cells to the frontier cells.
            var neighboringCellsToAddToFrontier = cell.NeighboringCells
                .Select(x => x.Value)
                .Where(x => !_frontierCells.Contains(x) && !_incorporatedCells.Contains(x));
            foreach (var cellToAdd in neighboringCellsToAddToFrontier)
            {
                cellToAdd.Background = Colors.LightCoral;
                _frontierCells.Add(cellToAdd);
            }
        }

        #endregion
    }
}
