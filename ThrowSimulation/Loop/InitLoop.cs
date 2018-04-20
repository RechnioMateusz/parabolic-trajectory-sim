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
        Canon canon;

        public InitLoop(uint width, uint height, string title) : base(width, height, title)
        {
        }

        protected override void Initialize()
        {
            drawer = new Drawer();
            canon = new Canon(new Point(100, 100), new Vector(100, 30), 30, 5);
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(double dt)
        {
            canon.Move(adapter.cursor);
        }

        protected override void Render(double leftover_time)
        {
            drawer.DrawCanon(window, canon);
        }
    }
}
