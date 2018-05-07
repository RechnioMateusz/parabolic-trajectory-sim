using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrowSimulation.BasicElements;

namespace ThrowSimulation.Objects
{
    class Scene
    {
        public Cannon cannon;
        public List<Projectile> projectiles = new List<Projectile>();
        private double width, height;
        private double gravity = 9.81,
            environment_density = 0.3,
            resistance_force = 0.45,
            shot_power = 2000,
            projectile_radius;

        public Scene(Cannon cannon, uint width, uint height)
        {
            this.cannon = cannon;
            this.width = width;
            this.height = height;
            projectile_radius = cannon.width;
        }

        public Scene(Cannon cannon, uint width, uint height, double gravity, double environment_density, double resistance_force, double shot_power)
        {
            this.cannon = cannon;
            this.width = width;
            this.height = height;
            projectile_radius = cannon.width;
            this.gravity = gravity;
            this.environment_density = environment_density;
            this.resistance_force = resistance_force;
            this.shot_power = shot_power;
        }

        public bool Shoot(bool mouse_click, Point mouse_position)
        {
            if (mouse_click)
            {
                Vector direction = new Vector(cannon.hitch, mouse_position);
                direction.ToUnitary();
                direction = direction * shot_power;
                projectiles.Add(new Projectile(new Point(cannon.hitch.x, cannon.hitch.y), cannon.width, direction, gravity, (-environment_density * resistance_force)));

                return true;
            }
            return false;
        }

        public void UpdateProjectiles()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles.ElementAt(i).MoveFor();
            }

            int j = 0;
            while (j < projectiles.Count)
            {
                if (projectiles.ElementAt(j).hitch.x > width 
                    || projectiles.ElementAt(j).hitch.y > height 
                    || projectiles.ElementAt(j).hitch.x < 0)
                {
                    projectiles.RemoveAt(j);
                }
                j++;
            }
        }
    }
}
