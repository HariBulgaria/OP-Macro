using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OP_Macro
{
    public class Coord
    {
        public int Id { get; set; } // ID of coords 
        public int X { get; set; } // X-coordinate
        public int Y { get; set; } // Y-coordinate
        public Coord(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}
