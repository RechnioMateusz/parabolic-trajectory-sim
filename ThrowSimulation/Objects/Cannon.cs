using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrowSimulation.BasicElements;

namespace ThrowSimulation.Objects
{
    /// <summary>
    /// Class representing cannon
    /// </summary>
    class Cannon
    {
        public Point hitch;
        public Point[] shape;
        public double length, width;

        /// <summary>
        /// Constructor alloting shape of cannon
        /// </summary>
        /// <param name="hitch"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        public Cannon(Point hitch, double length, double width)
        {
            shape = new Point[4];
            this.hitch = hitch;
            this.length = length;
            this.width = width;

            Vector direction = new Vector(1, 0);
            Point temp = new Point(hitch.x + (direction.x * length), hitch.y + (direction.y * length));
            direction.Rotate(90);

            shape[0] = new Point(hitch.x + (direction.x * width), hitch.y + (direction.y * width));
            shape[1] = new Point(temp.x + (direction.x * width), temp.y + (direction.y * width));
            shape[2] = new Point(temp.x - (direction.x * width), temp.y - (direction.y * width));
            shape[3] = new Point(hitch.x - (direction.x * width), hitch.y - (direction.y * width));
        }

        /// <summary>
        /// Rotates cannon to direction given by mouse cursor
        /// </summary>
        /// <param name="cursor"></param>
        public void Rotate(Point cursor)
        {
            Vector direction = new Vector(hitch, cursor);

            direction.ToUnitary();
            Point temp = new Point(hitch.x + (direction.x * length), hitch.y + (direction.y * length));
            direction.Rotate(90);

            shape[0].MoveTo(hitch.x + (direction.x * width), hitch.y + (direction.y * width));
            shape[1].MoveTo(temp.x + (direction.x * width), temp.y + (direction.y * width));
            shape[2].MoveTo(temp.x - (direction.x * width), temp.y - (direction.y * width));
            shape[3].MoveTo(hitch.x - (direction.x * width), hitch.y - (direction.y * width));
        }

        /// <summary>
        /// Reshapes cannon to given length and width
        /// </summary>
        /// <param name="length"></param>
        /// <param name="width"></param>
        public void ReshapeTo(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        /// <summary>
        /// Reshapes cannon length + value and width + value
        /// </summary>
        /// <param name="length"></param>
        /// <param name="width"></param>
        public void ReshapeFor(double length, double width)
        {
            this.length += length;
            this.width += width;
        }
    }
}
