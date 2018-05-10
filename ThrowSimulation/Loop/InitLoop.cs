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
        Font font;

        public InitLoop(uint width, uint height, string title) : base(width, height, title)
        {
        }

        protected override void LoadContent()
        {
        }

        protected override void Initialize()
        {
            drawer = new Drawer();
            new_scene = new Scene(new Cannon(new Point(100, 700), 30, 4), width, height);
            font = new Font("content/ostrich-regular.ttf");
        }

        protected override void Update(double dt)
        {
            new_scene.UpdateProjectiles();
            new_scene.cannon.Rotate(adapter.cursor);
            new_scene.ClearProjectiles(adapter.clear);
            new_scene.Shoot(adapter.LMB_click, adapter.cursor);
            new_scene.AddOrSubstract(adapter.cursor, adapter.wheel_moved);
            new_scene.MoveCannon(adapter.RMB_click, adapter.cursor);

            //Adapter fix
            adapter.LMB_click = false;
            adapter.RMB_click = false;
            adapter.wheel_moved = 0;
        }

        protected override void Render(double leftover_time)
        {
            drawer.DrawScene(window, new_scene, font, adapter.vectors, adapter.fill);
        }
    }
}
