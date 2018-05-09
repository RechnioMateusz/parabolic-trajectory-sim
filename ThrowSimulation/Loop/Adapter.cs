using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrowSimulation.BasicElements;

namespace ThrowSimulation.Loop
{
    class Adapter
    {
        public Point cursor = new Point(0, 0);
        public bool LMP_click = false;
        public char key = 'x';
        public int vectors = 1;
        public int fill = 1;
        public bool clear = false;
    }
}
