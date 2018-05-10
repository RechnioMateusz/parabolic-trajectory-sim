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
        public void DrawCanon(RenderWindow window, Cannon cannon)
        {
            VertexArray ver_arr = new VertexArray(PrimitiveType.Quads);

            for (int i = 0; i < cannon.shape.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)cannon.shape[i].x, (float)cannon.shape[i].y), Color.Cyan));
            }
            ver_arr.Append(new Vertex(new Vector2f((float)cannon.shape[0].x, (float)cannon.shape[0].y), Color.Cyan));

            window.Draw(ver_arr);
        }

        public void DrawProjectile(RenderWindow window, Projectile projectile, int fill)
        {
            Circle circle = new Circle(projectile.hitch, projectile.radius, 10);
            VertexArray ver_arr = new VertexArray(PrimitiveType.TrianglesFan);
            if (fill == -1)
            {
                ver_arr = new VertexArray(PrimitiveType.LinesStrip);
            }

            for (int i = 0; i < circle.vertices.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)circle.vertices[i].x, (float)circle.vertices[i].y), Color.Magenta));
            }

            ver_arr.Append(new Vertex(new Vector2f((float)circle.vertices[0].x, (float)circle.vertices[0].y), Color.Magenta));
            window.Draw(ver_arr);
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

        public void DrawText(RenderWindow window, Font font, string value, uint size, Point position)
        {
            Text text = new Text(value, font, size);
            text.Position = new Vector2f((float)(position.x), (float)(position.y));
            window.Draw(text);
        }

        public void DrawSceneInfo(RenderWindow window, Font font, Scene scene)
        {
            DrawText(window, font, "Gravity (m/s^2): " + scene.gravity.ToString(), 20, new Point(20, 20));
            DrawText(window, font, "Environment density (kg/m^3): " + scene.environment_density.ToString(), 20, new Point(20, 40));
            DrawText(window, font, "Shot power: " + scene.shot_power.ToString(), 20, new Point(20, 60));
            DrawText(window, font, "Projectile radius (m): " + scene.projectile_radius.ToString(), 20, new Point(20, 80));
            DrawText(window, font, "Projectile mass (kg): " + scene.projectile_mass.ToString(), 20, new Point(20, 100));
            DrawText(window, font, "Resistance force: " + scene.resistance_force.ToString(), 20, new Point(20, 120));
        }

        public void DrawScene(RenderWindow window, Scene scene, Font font, int vectors, int fill)
        {
            DrawCanon(window, scene.cannon);
            for (int i = 0; i < scene.projectiles.Count; i++)
            {
                DrawProjectile(window, scene.projectiles.ElementAt(i), fill);
                if (vectors == -1)
                {
                    DrawVectorsField(window, scene.projectiles.ElementAt(i));
                }
            }
            DrawSceneInfo(window, font, scene);
        }
    }
}
