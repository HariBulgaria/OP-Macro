using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OP_Macro
{
    public class MouseEventRecord
    {
        public int X { get; set; }       // X-coordinate of the mouse click
        public int Y { get; set; }       // Y-coordinate of the mouse click
        public int Delay { get; set; }   // Delay in milliseconds before this event is replayed
    }
}
