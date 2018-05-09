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
            new_scene = new Scene(new Cannon(new Point(100, 700), new Vector(100, 30), 30, 5), width, height);
            font = new Font("content/creditva.ttf");
        }

        protected override void Update(double dt)
        {
            new_scene.UpdateProjectiles();
            new_scene.cannon.Move(adapter.cursor);
            new_scene.ClearProjectiles(adapter.clear);
            new_scene.AddOrSubstract(adapter.key);
            bool shot = new_scene.Shoot(adapter.LMP_click, adapter.cursor);
            if (shot)
            {
                adapter.LMP_click = false;
            }
        }

        protected override void Render(double leftover_time)
        {
            drawer.DrawScene(window, new_scene, font, adapter.vectors, adapter.fill);
        }
    }
}
