using System.Collections.Generic;
using System.Linq;
using MazeGenAndPathFinding.Model.DataModels;

namespace MazeGenAndPathFinding.Model.MazeGeneration.Algorithms
{
    public class ReverseBacktracking : MazeGenerationAlgoithmBase
    {
        #region Constructor

        public ReverseBacktracking()
        {
            Name = "Reverse Backtracking";
        }

        #endregion

        #region Methods

        public override void ResetMaze()
        {
            Maze.ResetAllInteriorWalls(false);
            Maze.OnCellsChanged(null);
        }

        public override void GenerateMaze()
        {
            Maze.ResetAllInteriorWalls(false);

            var currentChain = new Stack<Cell>();
            var visitedCells = new HashSet<Cell>();

            var currentCell = GetRandomCell();

            while(true)
            {
                visitedCells.Add(currentCell);
                var neighboringCells = Maze.GetNeighboringCells(currentCell)
                    .Where(x => !visitedCells.Contains(x.Value))
                    .ToList();
                if (neighboringCells.Any())
                {
                    var randomNeighbor = neighboringCells.ElementAt(Random.Next(0, neighboringCells.Count));
                    currentCell.BreakWall(randomNeighbor.Key);
                    currentChain.Push(currentCell);
                    currentCell = randomNeighbor.Value;
                }
                else
                {
                    if (currentChain.Any())
                    {
                        currentCell = currentChain.Pop();
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Maze.OnCellsChanged(null);
        }

        #endregion
    }
}
