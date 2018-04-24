using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using ThrowSimulation.BasicElements;
using ThrowSimulation.Objects;

namespace ThrowSimulation.Loop
{
    class Drawer
    {
        public void DrawCanon(RenderWindow window, Cannon canon)
        {
            VertexArray ver_arr = new VertexArray(PrimitiveType.LinesStrip);

            for (int i = 0; i < canon.shape.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)canon.shape[i].x, (float)canon.shape[i].y), Color.Cyan));
            }
            ver_arr.Append(new Vertex(new Vector2f((float)canon.shape[0].x, (float)canon.shape[0].y), Color.Cyan));

            window.Draw(ver_arr);
        }

        public void DrawProjectile(RenderWindow window, Projectile projectile)
        {
            CircleShape circle = new CircleShape((float)projectile.radius, 30);
            circle.Position = new Vector2f((float)projectile.hitch.x, (float)projectile.hitch.y);
            circle.FillColor = new Color(255, 255, 255);
            window.Draw(circle);
        }

        public void DrawVectorsField(RenderWindow window, Projectile projectile)
        {
            VertexArray ver_arr = new VertexArray(PrimitiveType.Lines);

            for (int i = 0; i < projectile.vectors.const_forces.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)projectile.hitch.x, (float)projectile.hitch.y), Color.Green));
                ver_arr.Append(new Vertex(new Vector2f((float)(projectile.hitch.x + projectile.vectors.const_forces[i].x), (float)(projectile.hitch.y + projectile.vectors.const_forces[i].y)), Color.Green));
            }

            ver_arr.Append(new Vertex(new Vector2f((float)projectile.hitch.x, (float)projectile.hitch.y), Color.Magenta));
            ver_arr.Append(new Vertex(new Vector2f((float)(projectile.hitch.x + projectile.vectors.momentum.x), (float)(projectile.hitch.y + projectile.vectors.momentum.y)), Color.Magenta));

            window.Draw(ver_arr);
        }
    }
}
