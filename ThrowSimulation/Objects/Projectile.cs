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
        public double radius, mass;
        public VectorsField vectors;

        public Projectile(Point hitch, double radius, Vector starting_momentum, double mass, double gravity, double air_res_without_vel, double displacement_force)
        {
            this.hitch = hitch;
            this.radius = radius;
            this.mass = mass;
            vectors = new VectorsField(starting_momentum, (gravity * mass) / 60, air_res_without_vel / 60, displacement_force / 60);
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
