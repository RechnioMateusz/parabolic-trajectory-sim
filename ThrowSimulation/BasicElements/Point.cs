using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    /// <summary>
    /// Class representing point in Euclidean space
    /// </summary>
    class Point
    {
        public double x, y;

        /// <summary>
        /// Constructor for point class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Moves point to new location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveTo(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Moves point for given x and y
        /// </summary>
        /// <param name="delta_x"></param>
        /// <param name="delta_y"></param>
        public void MoveFor(double delta_x, double delta_y)
        {
            x += delta_x;
            y += delta_y;
        }
    }
}
