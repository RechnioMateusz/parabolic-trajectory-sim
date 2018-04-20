using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrowSimulation.BasicElements;

namespace ThrowSimulation.Objects
{
    class Projectile
    {
        public Point hitch;
        public double radius;
        public Vector momentum_vector;

        public Projectile(Point hitch, double radius)
        {
            this.hitch = hitch;
            this.radius = radius;
            momentum_vector = new Vector();
        }

        private Vector CalculateAccidentalVector(List<Vector> input_vectors)
        {
            Vector temp = new Vector();

            for (int i = 0; i < input_vectors.Count; i++)
            {
                temp += input_vectors[i];
            }

            return temp;
        }

        public void MoveFor(List<Vector> input_vectors)
        {
            momentum_vector += CalculateAccidentalVector(input_vectors);
            hitch.MoveFor(momentum_vector.x, momentum_vector.y);
        }
    }
}
