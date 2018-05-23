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
    /// <summary>
    /// Class inheriting from MainLoop class
    /// </summary>
    class InitLoop : MainLoop
    {
        Drawer drawer;
        Scene scene;
        Font font;

        /// <summary>
        /// Constructor taking width, height and title of new window
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="title"></param>
        public InitLoop(uint width, uint height, string title) : base(width, height, title)
        {
        }

        /// <summary>
        /// Loads content before program main loop starts
        /// </summary>
        protected override void LoadContent()
        {
        }

        /// <summary>
        /// Initialize loaded content
        /// </summary>
        protected override void Initialize()
        {
            drawer = new Drawer();
            scene = new Scene(new Cannon(new Point(100, 700), 30, 4), width, height);
            font = new Font("content/ostrich-regular.ttf");
        }

        /// <summary>
        /// Updates logic
        /// </summary>
        /// <param name="dt"></param>
        protected override void Update(double dt)
        {
            scene.UpdateProjectiles();
            scene.cannon.Rotate(adapter.cursor);
            scene.ClearProjectiles(adapter.clear);
            scene.Shoot(adapter.LMB_click, adapter.cursor);
            scene.ResolveCollisions();
            scene.AdjustParameters(adapter.cursor, adapter.wheel_moved);
            scene.MoveCannon(adapter.RMB_click, adapter.cursor);

            //Adapter fix
            adapter.LMB_click = false;
            adapter.RMB_click = false;
            adapter.wheel_moved = 0;
        }

        /// <summary>
        /// Updates UI
        /// </summary>
        /// <param name="leftover_time"></param>
        protected override void Render(double leftover_time)
        {
            drawer.DrawScene(window, scene, font, adapter.vectors, adapter.fill);
        }
    }
}
