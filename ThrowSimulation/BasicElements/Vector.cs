using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    /// <summary>
    /// Class representing vector in Euclidean space
    /// </summary>
    class Vector
    {
        public double x, y, length;

        /// <summary>
        /// Constructor creating vector with given x and y direction
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
            CalculateLength();
        }

        /// <summary>
        /// Constructor creating zeroth vector
        /// </summary>
        public Vector()
        {
            x = 0;
            y = 0;
            length = 0;
        }

        /// <summary>
        /// Constructor creating vector from point a to point b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Vector(Point a, Point b)
        {
            x = b.x - a.x;
            y = b.y - a.y;
            CalculateLength();
        }

        /// <summary>
        /// x: {x}   y: {y}
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "x: " + x.ToString() + "\ty: " + y.ToString();
        }

        /// <summary>
        /// Calculates length of vector
        /// </summary>
        public void CalculateLength()
        {
            length = Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// Updates vector to unitary vector
        /// </summary>
        public void ToUnitary()
        {
            x /= length;
            y /= length;
        }

        /// <summary>
        /// Returns new unitary vector with direction of old vector
        /// </summary>
        /// <returns></returns>
        public Vector ReturnUnitary()
        {
            return new Vector(x / length, y / length);
        }

        /// <summary>
        /// Calculates dot product of 2 vectors
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public double DotProduct(Vector vec)
        {
            return x * vec.x + y * vec.y;
        }

        /// <summary>
        /// Rotates vector for given degree
        /// </summary>
        /// <param name="degree"></param>
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

        public static Vector operator -(Vector vec1, Vector vec2)
        {
            return new Vector(vec1.x - vec2.x, vec1.y - vec1.y);
        }

        public static Vector operator *(Vector vec1, double num)
        {
            return new Vector(vec1.x * num, vec1.y * num);
        }

        public static Vector operator *(double num, Vector vec1)
        {
            return new Vector(vec1.x * num, vec1.y * num);
        }

        public static Vector operator *(Vector vec1, int num)
        {
            return new Vector(vec1.x * num, vec1.y * num);
        }

        public static Vector operator *(int num, Vector vec1)
        {
            return new Vector(vec1.x * num, vec1.y * num);
        }

        public static Vector operator /(double num, Vector vec1)
        {
            if (vec1.x == 0)
            {
                return new Vector(num / 00000000000000000000000.1, num / vec1.y);
            }
            else if (vec1.y == 0)
            {
                return new Vector(num / vec1.x, num / 00000000000000000000000.1);
            }
            else
            {
                return new Vector(num / vec1.x, num / vec1.y);
            }
        }

        public static Vector operator /(Vector vec1, double num)
        {
            if (num == 0)
            {
                return new Vector(vec1.x / 00000000000000000000000.1, vec1.y / 00000000000000000000000.1);
            }
            else
            {
                return new Vector(vec1.x / num, vec1.y / num);
            }
        }
    }
}
