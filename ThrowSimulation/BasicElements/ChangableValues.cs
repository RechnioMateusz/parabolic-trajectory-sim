using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    struct ChangableValues
    {
        public Point hitch;
        public double width, height, value;

        public ChangableValues(Point hitch, double width, double height, double value)
        {
            this.hitch = hitch;
            this.width = width;
            this.height = height;
            this.value = value;
        }
    }
}
