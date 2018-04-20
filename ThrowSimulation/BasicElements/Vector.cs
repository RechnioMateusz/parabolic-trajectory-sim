using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    class Vector
    {
        public double x, y, length;

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
            CalculateLength();
        }

        public Vector()
        {
            x = 0;
            y = 0;
            length = 0;
        }

        public Vector(Point a, Point b)
        {
            x = b.x - a.x;
            y = b.y - a.y;
            CalculateLength();
        }

        public void CalculateLength()
        {
            length = Math.Sqrt(x * x + y * y);
        }

        public void ToUnitary()
        {
            x /= length;
            y /= length;
        }

        public double DotProduct(Vector vec)
        {
            return x * vec.x + y * vec.y;
        }

        public void Rotate(double degree)
        {
            degree = degree * Math.PI / 180.0f;
            double temp_x = x * Math.Cos(degree) - y * Math.Sin(degree);
            double temp_y = x * Math.Sin(degree) + y * Math.Cos(degree);
            x = temp_x;
            y = temp_y;
        }

        public static Vector operator +(Vector vec1, Vector vec2)
        {
            return new Vector(vec1.x + vec2.x, vec1.y + vec2.y);
        }
    }
}
