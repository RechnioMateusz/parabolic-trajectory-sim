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
        Projectile projectile;
        List<Vector> vectors;

        public InitLoop(uint width, uint height, string title) : base(width, height, title)
        {
        }

        protected override void Initialize()
        {
            drawer = new Drawer();
            canon = new Canon(new Point(100, 100), new Vector(100, 30), 30, 5);
            projectile = new Projectile(new Point(300, 300), 30);

            vectors = new List<Vector>();
            vectors.Add(new Vector(-2.0 / 60.0, -2.0 / 60.0));
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(double dt)
        {
            canon.Move(adapter.cursor);
            projectile.MoveFor(vectors);
        }

        protected override void Render(double leftover_time)
        {
            drawer.DrawCanon(window, canon);
            drawer.DrawProjectile(window, projectile);
        }
    }
}
