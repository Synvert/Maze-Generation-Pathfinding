using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGenAndPathFinding.Model.DataModels;

namespace MazeGenAndPathFinding.Model.MazeGeneration.Algorithms
{
    public class ReverseBacktracking : IMazeGenerationAlgorithm
    {
        #region Fields

        private Maze _maze;

        #endregion

        #region Constructor

        public ReverseBacktracking()
        {
            Name = "Reverse Backtracking";
            Description = "Placeholder Description";
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public string Description { get; private set; }

        #endregion

        #region Methods

        public void Initialize(int height, int width)
        {
            _maze = new Maze(height, width);

            //Populate maze walls and stuff
        }

        public Maze StepThrough(out bool isComplete)
        {
            if (_maze == null)
                throw new InvalidOperationException("Algorithm not initialized.");
            throw new NotImplementedException();
        }

        public Maze GenerateMaze()
        {
            if (_maze == null)
                throw new InvalidOperationException("Algorithm not initialized.");
            throw new NotImplementedException();
        }

        #endregion
    }
}
