namespace MazeGenAndPathFinding.Model.PathFinding.Algorithms
{
    public class AStar : IPathFindingAlgorithm
    {
        public string Name { get; }

        public AStar()
        {
            Name = "A*";
        }
    }
}
