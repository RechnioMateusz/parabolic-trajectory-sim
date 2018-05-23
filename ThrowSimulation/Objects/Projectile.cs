using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrowSimulation.BasicElements;

namespace ThrowSimulation.Objects
{
    /// <summary>
    /// Class representing projectile
    /// </summary>
    class Projectile
    {
        public Point hitch;
        public double radius, mass, mass_inverted, restitution;
        public VectorsField vectors;
        public byte[] color = new byte[3];

        /// <summary>
        /// Constructor that gets paramaters provided by scene
        /// </summary>
        /// <param name="hitch"></param>
        /// <param name="radius"></param>
        /// <param name="starting_momentum"></param>
        /// <param name="mass"></param>
        /// <param name="restitution"></param>
        /// <param name="gravity"></param>
        /// <param name="air_res_without_vel"></param>
        /// <param name="displacement_force"></param>
        /// <param name="color"></param>
        public Projectile(Point hitch, double radius, Vector starting_momentum, double mass, double restitution, double gravity, double air_res_without_vel, double displacement_force, byte[] color)
        {
            this.hitch = hitch;
            this.radius = radius;
            this.mass = mass;
            this.restitution = restitution;
            mass_inverted = 1 / mass;
            vectors = new VectorsField(starting_momentum, (gravity * mass) / 60, air_res_without_vel / 60, displacement_force / 60);
            this.color = color;
        }

        /// <summary>
        /// Calculates vectors created by for example collisions
        /// </summary>
        /// <param name="input_vector"></param>
        public void CalculateAccidentalVector(Vector input_vector)
        {
            vectors.forces.Add(input_vector);
        }

        /// <summary>
        /// Moves projectile along momentum vector direction
        /// </summary>
        public void Move()
        {
            vectors.UpdateMomentum();
            hitch.MoveFor(vectors.momentum.x / 60.0, vectors.momentum.y / 60.0);
        }
    }
}
