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
        public double radius, mass, mass_inverted, restitution;
        public VectorsField vectors;

        public Projectile(Point hitch, double radius, Vector starting_momentum, double mass, double restitution, double gravity, double air_res_without_vel, double displacement_force)
        {
            this.hitch = hitch;
            this.radius = radius;
            this.mass = mass;
            this.restitution = restitution;
            mass_inverted = 1 / mass;
            vectors = new VectorsField(starting_momentum, (gravity * mass) / 60, air_res_without_vel / 60, displacement_force / 60);
        }

        public void CalculateAccidentalVector(Vector input_vector)
        {
            vectors.forces.Add(input_vector);
        }

        public void Move()
        {
            vectors.UpdateMomentum();
            hitch.MoveFor(vectors.momentum.x / 60.0, vectors.momentum.y / 60.0);
        }
    }
}
