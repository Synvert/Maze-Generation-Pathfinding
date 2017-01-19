using System.Collections.Generic;

namespace MazeGenAndPathFinding.Model.DataModels
{
    public class Cell
    {
        public Dictionary<Direction, Wall> Walls { get; set; }
        public Dictionary<Direction, Cell> Cells { get; set; }

        public void BreakWall(Direction direction)
        {
            Walls[direction].IsBroken = true;
        }
    }
}
