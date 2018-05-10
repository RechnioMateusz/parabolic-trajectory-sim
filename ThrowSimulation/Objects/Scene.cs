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
        public double gravity = 9.81,
            environment_density = 1.9,
            resistance_force = 0.2,
            shot_power = 2000,
            projectile_radius,
            displacement_force,
            projectile_mass = 300;

        public Scene(Cannon cannon, uint width, uint height)
        {
            this.cannon = cannon;
            this.width = width;
            this.height = height;
            projectile_radius = cannon.width;
            displacement_force = environment_density * gravity * Math.PI * Math.Pow(projectile_radius, 2);
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
            displacement_force = environment_density * gravity * Math.PI * Math.Pow(projectile_radius, 2);
        }

        public bool Shoot(bool mouse_click, Point mouse_position)
        {
            if (mouse_click)
            {
                Vector direction = new Vector(cannon.hitch, mouse_position);
                direction.ToUnitary();
                direction = direction * shot_power;
                projectiles.Add(new Projectile(new Point(cannon.hitch.x, cannon.hitch.y), cannon.width, direction, projectile_mass, gravity, (-environment_density * resistance_force * projectile_radius), displacement_force));

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
                if (projectiles.ElementAt(j).hitch.x > width + projectiles.ElementAt(j).radius 
                    || projectiles.ElementAt(j).hitch.y > height + projectiles.ElementAt(j).radius
                    || projectiles.ElementAt(j).hitch.x < 0 - projectiles.ElementAt(j).radius
                    || projectiles.ElementAt(j).hitch.y < -250)
                {
                    projectiles.RemoveAt(j);
                }
                j++;
            }
        }

        public void ClearProjectiles(bool do_it)
        {
            if (do_it)
            {
                projectiles.Clear();
            }
        }

        public void AddOrSubstract(char key)
        {
            switch (key)
            {
                case 'q':
                    gravity += 0.1;
                    break;
                case 'a':
                    gravity -= 0.1;
                    break;
                case 'w':
                    environment_density += 0.001;
                    break;
                case 's':
                    environment_density -= 0.001;
                    break;
                case 'e':
                    shot_power += 50;
                    break;
                case 'd':
                    shot_power -= 50;
                    break;
                case 'r':
                    projectile_radius += 1;
                    cannon.ReshapeTo(cannon.length, projectile_radius);
                    break;
                case 'f':
                    projectile_radius -= 1;
                    cannon.ReshapeTo(cannon.length, projectile_radius);
                    break;
                case 't':
                    projectile_mass += 1;
                    break;
                case 'g':
                    projectile_mass -= 1;
                    break;
                case 'y':
                    resistance_force += 0.1;
                    break;
                case 'h':
                    resistance_force -= 0.1;
                    break;
                default:
                    break;
            }
        }
    }
}
