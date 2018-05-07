using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using ThrowSimulation.BasicElements;
using ThrowSimulation.Objects;

namespace ThrowSimulation.Loop
{
    class InitLoop : MainLoop
    {
        Drawer drawer;
        Scene new_scene;

        public InitLoop(uint width, uint height, string title) : base(width, height, title)
        {
        }

        protected override void LoadContent()
        {
        }

        protected override void Initialize()
        {
            drawer = new Drawer();
            new_scene = new Scene(new Cannon(new Point(100, 700), new Vector(100, 30), 30, 5), width, height);
        }

        protected override void Update(double dt)
        {
            new_scene.UpdateProjectiles();
            new_scene.cannon.Move(adapter.cursor);
            bool shot = new_scene.Shoot(adapter.LMP_click, adapter.cursor);
            if (shot)
            {
                adapter.LMP_click = false;
            }
        }

        protected override void Render(double leftover_time)
        {
            drawer.DrawCanon(window, new_scene.cannon);
            for (int i = 0; i < new_scene.projectiles.Count; i++)
            {
                drawer.DrawProjectile(window, new_scene.projectiles.ElementAt(i));
                //drawer.DrawVectorsField(window, new_scene.projectiles.ElementAt(i));
            }
        }
    }
}
