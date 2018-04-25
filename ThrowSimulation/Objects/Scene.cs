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

        public Scene(Cannon canon, uint width, uint height)
        {
            this.cannon = canon;
            this.width = width;
            this.height = height;
        }

        public bool Shoot(bool mouse_click, Point mouse_position)
        {
            if (mouse_click)
            {
                Vector direction = new Vector(cannon.hitch, mouse_position);
                projectiles.Add(new Projectile(new Point(cannon.hitch.x, cannon.hitch.y), cannon.width, direction));

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
