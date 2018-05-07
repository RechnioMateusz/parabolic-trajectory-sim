using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrowSimulation.BasicElements;

namespace ThrowSimulation.Objects
{
    class Cannon
    {
        public Point hitch;
        public Point[] shape;
        public double length, width;

        public Cannon(Point hitch, Vector direction, double length, double width)
        {
            shape = new Point[4];
            this.hitch = hitch;
            this.length = length;
            this.width = width;

            direction.ToUnitary();
            Point temp = new Point(hitch.x + (direction.x * length), hitch.y + (direction.y * length));
            direction.Rotate(90);

            shape[0] = new Point(hitch.x + (direction.x * width), hitch.y + (direction.y * width));
            shape[1] = new Point(temp.x + (direction.x * width), temp.y + (direction.y * width));
            shape[2] = new Point(temp.x - (direction.x * width), temp.y - (direction.y * width));
            shape[3] = new Point(hitch.x - (direction.x * width), hitch.y - (direction.y * width));
        }

        public void Move(Point cursor)
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

        public void ReshapeTo(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public void ReshapeFor(double length, double width)
        {
            this.length += length;
            this.width += width;
        }
    }
}
