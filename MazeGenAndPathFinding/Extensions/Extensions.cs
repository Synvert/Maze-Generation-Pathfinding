using MazeGenAndPathFinding.Models;

namespace MazeGenAndPathFinding.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Finds the opposite <see cref="Direction"/> of <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction">The <see cref="Direction"/> to find the opposite of.</param>
        /// <returns>The direction opposite of <paramref name="direction"/>.</returns>
        public static Direction GetOpposite(this Direction direction)
        {
            return (Direction)(((int)direction + 2) % 4);
        }
    }
}
