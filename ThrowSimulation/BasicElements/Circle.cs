using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    class Circle
    {
        public Point center;
        public Point[] vertices;
        private double radius;

        public Circle(Point center, double radius, int vertices_amount)
        {
            this.center = center;
            this.radius = radius;
            vertices = new Point[vertices_amount];

            Vector temp = new Vector(1, 0);
            temp = temp * radius;
            double angle = 360.0 / (double)vertices_amount;

            for (int i = 0; i < vertices_amount; i++)
            {
                vertices[i] = new Point(center.x + temp.x, center.y + temp.y);
                temp.Rotate(angle);
            }
        }
    }
}
