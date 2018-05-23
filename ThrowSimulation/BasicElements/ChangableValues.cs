using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    /// <summary>
    /// Class representing scene modifiable parameters with position
    /// </summary>
    struct ChangableValues
    {
        public Point hitch;
        public double width, height, value;

        /// <summary>
        /// Paramateres constructor
        /// </summary>
        /// <param name="hitch"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="value"></param>
        public ChangableValues(Point hitch, double width, double height, double value)
        {
            this.hitch = hitch;
            this.width = width;
            this.height = height;
            this.value = value;
        }
    }
}
