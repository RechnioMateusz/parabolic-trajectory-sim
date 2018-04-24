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
        private double gravity = 16.7408;
        private double air_resistance = 3.4;

        public VectorsField(Vector momentum)
        {
            this.momentum = momentum;
            Vector gravity_vec = new Vector(0, 1);
            gravity_vec = gravity_vec * gravity;
            Vector air_res_vec = new Vector(-momentum.x, -momentum.y);
            air_res_vec.ToUnitary();
            air_res_vec = air_res_vec * air_resistance;
            const_forces[0] = gravity_vec;
            const_forces[1] = air_res_vec;
        }

        private void UpdateAirResistanceVec()
        {
            Vector temp = new Vector(-momentum.x, -momentum.y);
            temp.ToUnitary();
            temp = temp * air_resistance;
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
