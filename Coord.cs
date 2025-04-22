using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OP_Macro
{
    public class Coord
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Coord(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}
