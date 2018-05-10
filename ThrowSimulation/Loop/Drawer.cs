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
            Color color = new Color(255, 100, 100);

            for (int i = 0; i < cannon.shape.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)cannon.shape[i].x, (float)cannon.shape[i].y), color));
            }
            ver_arr.Append(new Vertex(new Vector2f((float)cannon.shape[0].x, (float)cannon.shape[0].y), color));

            window.Draw(ver_arr);
        }

        public void DrawProjectile(RenderWindow window, Projectile projectile, int fill)
        {
            Circle circle = new Circle(projectile.hitch, projectile.radius, 15);
            VertexArray ver_arr = new VertexArray(PrimitiveType.TrianglesFan);
            Color color = new Color(200, 200, 30);
            if (fill == -1)
            {
                ver_arr = new VertexArray(PrimitiveType.LinesStrip);
            }

            for (int i = 0; i < circle.vertices.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)circle.vertices[i].x, (float)circle.vertices[i].y), color));
            }

            ver_arr.Append(new Vertex(new Vector2f((float)circle.vertices[0].x, (float)circle.vertices[0].y), color));
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
            text.Color = new Color(50, 255, 130);
            window.Draw(text);
        }

        public void DrawSceneInfo(RenderWindow window, Font font, Scene scene)
        {
            VertexArray ver_arr = new VertexArray(PrimitiveType.Lines);
            Color line_fading_color = new Color(0, 150, 150, 10);
            Color line_color = new Color(0, 230, 230);


            for (int i = 0; i < 7; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f(0, (float)(i * scene.text_height + 10)),line_color ));
                ver_arr.Append(new Vertex(new Vector2f((float)scene.width, (float)(i * scene.text_height + 10)), line_fading_color));
            }
            window.Draw(ver_arr);

            DrawText(window, font, "Gravity: " + scene.gravity.value.ToString(), (uint)scene.gravity.height, scene.gravity.hitch);
            DrawText(window, font, "Environment density: " + scene.environment_density.value.ToString(), (uint)scene.environment_density.height, scene.environment_density.hitch);
            DrawText(window, font, "Shot power: " + scene.shot_power.value.ToString(), (uint)scene.shot_power.height, scene.shot_power.hitch);
            DrawText(window, font, "Projectile radius: " + scene.projectile_radius.value.ToString(), (uint)scene.projectile_radius.height, scene.projectile_radius.hitch);
            DrawText(window, font, "Projectile mass: " + scene.projectile_mass.value.ToString(), (uint)scene.projectile_mass.height, scene.projectile_mass.hitch);
            DrawText(window, font, "Resistance force: " + scene.resistance_force.value.ToString(), (uint)scene.resistance_force.height, scene.resistance_force.hitch);
        }

        public void DrawScene(RenderWindow window, Scene scene, Font font, int vectors, int fill)
        {
            for (int i = 0; i < scene.projectiles.Count; i++)
            {
                DrawProjectile(window, scene.projectiles.ElementAt(i), fill);
                if (vectors == -1)
                {
                    DrawVectorsField(window, scene.projectiles.ElementAt(i));
                }
            }
            DrawCanon(window, scene.cannon);
            DrawSceneInfo(window, font, scene);
        }
    }
}
