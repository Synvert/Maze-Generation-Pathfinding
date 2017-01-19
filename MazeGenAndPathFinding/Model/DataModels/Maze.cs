using System;

namespace MazeGenAndPathFinding.Model.DataModels
{
    public class Maze
    {
        public Maze(int height, int width)
        {
            if (height <= 1 || height % 2 != 1)
                throw new ArgumentException("Must be an odd positive integer greater than 1.", "height");
            if (width <= 1 || width % 2 != 1)
                throw new ArgumentException("Must be an odd positive integer greater than 1.", "width");
            Height = height;
            Width = width;
            Cells = new Cell[height, width];
        }
        
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Cell[,] Cells { get; set; }
    }
}
