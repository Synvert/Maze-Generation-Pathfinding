using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenAndPathFinding.Model.DataModels
{
    public class Wall
    {
        public Wall(bool isBreakable)
        {
            IsBreakable = isBreakable;
            IsBroken = false;
        }

        public bool IsBroken { get; set; }
        public bool IsBreakable { get; set; }     
    }
}
