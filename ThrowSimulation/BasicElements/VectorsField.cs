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
        public Vector[] const_forces = new Vector[2];
        private double gravity, air_res_without_velocity;

        public VectorsField(Vector momentum, double gravity, double air_res_without_velocity)
        {
            this.momentum = momentum;
            Vector gravity_vec = new Vector(0, 1);
            gravity_vec = gravity_vec * gravity;
            Vector air_res_vec = air_res_without_velocity * momentum;
            const_forces[0] = gravity_vec;
            const_forces[1] = air_res_vec;
        }

        private void UpdateAirResistanceVec()
        {
            Vector temp = air_res_without_velocity * momentum;
            const_forces[1] = temp;
        }

        public void UpdateMomentum()
        {
            Vector temp = new Vector();

            for (int i = 0; i < const_forces.Length; i++)
            {
                temp += const_forces[i];
            }

            for (int i = 0; i < forces.Count; i++)
            {
                temp += forces.ElementAt(i);
            }
            forces.Clear();

            momentum += temp;
            UpdateAirResistanceVec();
        }

        public void AddNewForce(Vector force)
        {
            forces.Add(force);
        }
    }
}
