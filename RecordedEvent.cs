using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OP_Macro
{
    public class RecordedEvent
    {
        public uint Message { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public uint Key { get; set; }
        public int Delay { get; set; }

        public RecordedEvent(uint message, int x, int y, uint key, int delay)
        {
            Message = message;
            X = x;
            Y = y;
            Key = key;
            Delay = delay;
        }

        public RecordedEvent() { }
    }
}
