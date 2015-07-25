using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenAndPathFinding.Model.DataModels
{
    public class Cell
    {
        public Wall North { get; set; }
        public Wall South { get; set; }
        public Wall East { get; set; }
        public Wall West { get; set; }
    }
}
