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
        private double cannon_scale;
        public List<Projectile> projectiles = new List<Projectile>();
        public double width, height, text_height = 30;
        public ChangableValues gravity, environment_density, resistance_force, shot_power, projectile_radius, projectile_mass, projectile_restitution;
        public double displacement_force;

        public Scene(Cannon cannon, uint width, uint height)
        {
            this.cannon = cannon;
            cannon_scale = cannon.length / cannon.width;
            this.width = width;
            this.height = height;
            gravity = new ChangableValues(new Point(20, 5), width, text_height, 9.81);
            environment_density = new ChangableValues(new Point(20, 35), width, text_height, 1.9);
            shot_power = new ChangableValues(new Point(20, 65), width, text_height, 2000);
            projectile_radius = new ChangableValues(new Point(20, 95), width, text_height, cannon.width);
            projectile_mass = new ChangableValues(new Point(20, 125), width, text_height, 300);
            resistance_force = new ChangableValues(new Point(20, 155), width, text_height, 0.2);
            projectile_restitution = new ChangableValues(new Point(20, 185), width, text_height, 10);
            displacement_force = environment_density.value * gravity.value * Math.PI * Math.Pow(projectile_radius.value, 2);
        }

        public Scene(Cannon cannon, uint width, uint height, double gravity, double environment_density, double resistance_force, double shot_power, double projectile_mass, double projectile_restitution)
        {
            this.cannon = cannon;
            cannon_scale = cannon.length / cannon.width;
            this.width = width;
            this.height = height;
            this.gravity = new ChangableValues(new Point(20, 5), width, text_height, gravity);
            this.environment_density = new ChangableValues(new Point(20, 35), width, text_height, environment_density);
            this.shot_power = new ChangableValues(new Point(20, 65), width, text_height, shot_power);
            this.projectile_radius = new ChangableValues(new Point(20, 95), width, text_height, cannon.width);
            this.projectile_mass = new ChangableValues(new Point(20, 125), width, text_height, projectile_mass);
            this.resistance_force = new ChangableValues(new Point(20, 155), width, text_height, resistance_force);
            this.projectile_restitution = new ChangableValues(new Point(20, 185), width, text_height, projectile_restitution);
            displacement_force = environment_density * gravity * Math.PI * Math.Pow(projectile_radius.value, 2);
        }

        public bool Shoot(bool LMB_click, Point mouse_position)
        {
            if (LMB_click)
            {
                Vector direction = new Vector(cannon.hitch, mouse_position);
                direction.ToUnitary();
                Vector projectile_start_position = direction;
                direction = direction * shot_power.value;
                projectile_start_position = projectile_start_position * cannon.length;

                projectiles.Add(new Projectile(new Point(cannon.hitch.x + projectile_start_position.x, cannon.hitch.y + projectile_start_position.y), 
                    cannon.width, direction, projectile_mass.value, projectile_restitution.value, gravity.value, 
                    (-environment_density.value * resistance_force.value * projectile_radius.value), displacement_force));

                return true;
            }
            return false;
        }

        public void MoveCannon(bool RMB_click, Point mouse_position)
        {
            if (RMB_click)
            {
                cannon = new Cannon(new Point(mouse_position.x, mouse_position.y), cannon.length, cannon.width);
            }
        }

        public void UpdateProjectiles()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles.ElementAt(i).Move();
            }

            int j = 0;
            while (j < projectiles.Count)
            {
                if (projectiles.ElementAt(j).hitch.x > width + projectiles.ElementAt(j).radius
                    || projectiles.ElementAt(j).hitch.y > height + projectiles.ElementAt(j).radius + 100
                    || projectiles.ElementAt(j).hitch.x < 0 - projectiles.ElementAt(j).radius
                    || projectiles.ElementAt(j).hitch.y < -250)
                {
                    projectiles.RemoveAt(j);
                }
                j++;
            }
        }

        public void ClearProjectiles(bool just_do_it)
        {
            if (just_do_it)
            {
                projectiles.Clear();
            }
        }

        public void ResolveCollisions()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                for (int j = i; j < projectiles.Count; j++)
                {
                    Projectile A = projectiles.ElementAt(i);
                    Projectile B = projectiles.ElementAt(j);
                    if (A != B)
                    {
                        Vector length_beteween_projectiles = new Vector(B.hitch, A.hitch);
                        double radius_sum = A.radius + B.radius;
                        if (length_beteween_projectiles.length <= radius_sum)
                        {
                            Vector normal = length_beteween_projectiles.ReturnUnitary();
                            Vector relative_velocity = B.vectors.momentum - A.vectors.momentum;
                            double velocity_along_normal = normal.DotProduct(relative_velocity);

                            //if (velocity_along_normal > 0)
                            //{
                            //    Console.WriteLine(velocity_along_normal);
                            //    return;
                            //}

                            double e = Math.Min(B.restitution, A.restitution);
                            double impulse_scalar = -(1 - e) * velocity_along_normal;
                            impulse_scalar /= A.mass_inverted + B.mass_inverted;

                            Vector impulse = impulse_scalar * normal;
                            B.CalculateAccidentalVector(impulse / B.mass);
                            A.CalculateAccidentalVector(impulse / A.mass * -1);
                        }
                    }
                }
            }
        }

        public void AdjustParameters(Point cursor, int delta)
        {
            if (cursor.x >= gravity.hitch.x && cursor.y >= gravity.hitch.y && 
                cursor.x <= gravity.hitch.x + gravity.width && cursor.y <= gravity.hitch.y + gravity.height)
            {
                gravity.value += delta * 0.1;
            }
            else if (cursor.x >= environment_density.hitch.x && cursor.y >= environment_density.hitch.y &&
                cursor.x <= environment_density.hitch.x + environment_density.width && cursor.y <= environment_density.hitch.y + environment_density.height)
            {
                environment_density.value += delta * 0.001;
            }
            else if (cursor.x >= shot_power.hitch.x && cursor.y >= shot_power.hitch.y &&
                cursor.x <= shot_power.hitch.x + shot_power.width && cursor.y <= shot_power.hitch.y + shot_power.height)
            {
                shot_power.value += delta * 50;
            }
            else if (cursor.x >= projectile_radius.hitch.x && cursor.y >= projectile_radius.hitch.y &&
                cursor.x <= projectile_radius.hitch.x + projectile_radius.width && cursor.y <= projectile_radius.hitch.y + projectile_radius.height)
            {
                projectile_radius.value += delta;
                cannon.ReshapeTo(cannon_scale * cannon.width, projectile_radius.value);
            }
            else if (cursor.x >= projectile_mass.hitch.x && cursor.y >= projectile_mass.hitch.y &&
                cursor.x <= projectile_mass.hitch.x + projectile_mass.width && cursor.y <= projectile_mass.hitch.y + projectile_mass.height)
            {
                projectile_mass.value += delta;
            }
            else if (cursor.x >= resistance_force.hitch.x && cursor.y >= resistance_force.hitch.y &&
                cursor.x <= resistance_force.hitch.x + resistance_force.width && cursor.y <= resistance_force.hitch.y + resistance_force.height)
            {
                resistance_force.value += delta * 0.1;
            }
            else if(cursor.x >= projectile_restitution.hitch.x && cursor.y >= projectile_restitution.hitch.y &&
                cursor.x <= projectile_restitution.hitch.x + projectile_restitution.width && cursor.y <= projectile_restitution.hitch.y + projectile_restitution.height)
            {
                projectile_restitution.value += delta;
            }
        }
    }
}
