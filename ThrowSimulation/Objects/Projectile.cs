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
        public VectorsField vectors;

        public Projectile(Point hitch, double radius, Vector starting_momentum)
        {
            this.hitch = hitch;
            this.radius = radius;
            vectors = new VectorsField(starting_momentum);
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

        public void MoveFor()
        {
            vectors.UpdateMomentum();
            hitch.MoveFor(vectors.momentum.x / 60.0, vectors.momentum.y / 60.0);
        }
    }
}
