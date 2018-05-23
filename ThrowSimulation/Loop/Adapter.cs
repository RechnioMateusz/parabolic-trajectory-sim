using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrowSimulation.BasicElements;

namespace ThrowSimulation.Loop
{
    /// <summary>
    /// Class containing variables used to comunicate input from keyboard or mouse with program
    /// </summary>
    class Adapter
    {
        public Point cursor = new Point(0, 0);
        public bool LMB_click = false;
        public bool RMB_click = false;
        public int vectors = 1;
        public int fill = 1;
        public bool clear = false;
        public int wheel_moved = 0;
    }
}
