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
        public void DrawCanon(RenderWindow window, Canon canon)
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
            circle.FillColor = new Color(255, 50, 255);
            window.Draw(circle);
        }
    }
}
