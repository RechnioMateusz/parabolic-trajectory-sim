using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    /// <summary>
    /// Class representing field of vectors interacting with projectile
    /// </summary>
    class VectorsField
    {
        public List<Vector> forces = new List<Vector>();
        public Vector momentum;
        public Vector[] const_forces = new Vector[3];
        private double air_res_without_velocity;

        /// <summary>
        /// Constructor that takes "constans forces provided by scene
        /// CHANGING SCENE PARAMETERS INFLUENCES ONLY NEW PROJECTILES"
        /// </summary>
        /// <param name="momentum"></param>
        /// <param name="gravity"></param>
        /// <param name="air_res_without_velocity"></param>
        /// <param name="displacement_force"></param>
        public VectorsField(Vector momentum, double gravity, double air_res_without_velocity, double displacement_force)
        {
            this.momentum = momentum;
            this.air_res_without_velocity = air_res_without_velocity;
            Vector gravity_vec = new Vector(0, gravity);
            Vector air_res_vec = air_res_without_velocity * momentum;
            Vector disp_force = new Vector(0, -displacement_force);
            const_forces[0] = gravity_vec;
            const_forces[1] = air_res_vec;
            const_forces[2] = disp_force;
        }

        /// <summary>
        /// Updates air resistance according to momentum
        /// </summary>
        private void UpdateAirResistanceVec()
        {
            const_forces[1] = air_res_without_velocity * momentum;
        }

        /// <summary>
        /// Updates momentum according to constans and accidental forces
        /// </summary>
        public void UpdateMomentum()
        {
            for (int i = 0; i < const_forces.Length; i++)
            {
                momentum = momentum + const_forces[i];
            }

            for (int i = 0; i < forces.Count; i++)
            {
                momentum = momentum + forces.ElementAt(i);
            }
            if (forces.Count != 0)
            {
                forces.Clear();
            }

            UpdateAirResistanceVec();
        }
    }
}
