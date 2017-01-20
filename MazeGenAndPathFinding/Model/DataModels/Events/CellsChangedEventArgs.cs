using System;
using System.Collections.Generic;

namespace MazeGenAndPathFinding.Model.DataModels.Events
{
    public class CellsChangedEventArgs : EventArgs
    {
        public IReadOnlyList<Cell> CellsChanged { get; }

        public CellsChangedEventArgs(IReadOnlyList<Cell> cells)
        {
            CellsChanged = cells;
        }
    }
}
