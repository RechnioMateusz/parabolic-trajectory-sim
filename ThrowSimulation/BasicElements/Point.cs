using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    class Point
    {
        public double x, y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void MoveTo(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void MoveFor(double delta_x, double delta_y)
        {
            x += delta_x;
            y += delta_y;
        }
    }
}
