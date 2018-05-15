using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThrowSimulation.BasicElements
{
    class VectorsField
    {
        public List<Vector> forces = new List<Vector>();
        public Vector momentum;
        public Vector[] const_forces = new Vector[3];
        private double air_res_without_velocity;

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

        private void UpdateAirResistanceVec()
        {
            const_forces[1] = air_res_without_velocity * momentum;
        }

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
            forces.Clear();
            
            UpdateAirResistanceVec();
        }
    }
}
