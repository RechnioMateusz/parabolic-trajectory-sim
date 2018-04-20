using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ThrowSimulation.Loop
{
    abstract class MainLoop
    {
        protected RenderWindow window;
        protected Color background_color;
        protected uint width, height;
        protected const double target_fps = 60;
        protected const double dt = 1 / target_fps;
        protected Adapter adapter = new Adapter();

        public MainLoop(uint width, uint height, string title)
        {
            this.width = width;
            this.height = height;
            window = new RenderWindow(new VideoMode(this.width, this.height), title, Styles.Titlebar, new ContextSettings { DepthBits = 24, AntialiasingLevel = 4 });
            this.background_color = Color.Black;

            window.KeyPressed += Window_KeyPressed;
            window.MouseButtonPressed += Window_MouseButtonPressed;
            window.MouseButtonReleased += Window_MouseButtonReleased;
            window.MouseMoved += Window_MouseMoved;
        }

        protected abstract void LoadContent();
        protected abstract void Initialize();
        protected abstract void Update(double dt);
        protected abstract void Render(double leftover_time);

        private void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            adapter.cursor.x = e.X;
            adapter.cursor.y = e.Y;
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                adapter.LMP = true;
            }
        }

        private void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                adapter.LMP = false;
            }
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        }

        public void RUN()
        {
            LoadContent();
            Initialize();

            Clock clock = new Clock();
            double previous_time = clock.ElapsedTime.AsSeconds();
            double accumulator = 0.0;

            while (window.IsOpen)
            {
                double current_time = clock.ElapsedTime.AsSeconds();
                double elapsed_time = current_time - previous_time;
                previous_time = current_time;
                accumulator += elapsed_time;

                window.Clear(background_color);
                window.DispatchEvents();

                if (accumulator > 0.25)
                {
                    accumulator = 0.25;
                }

                while (accumulator >= dt)
                {
                    Update(dt);
                    accumulator -= dt;
                }

                Render(accumulator / dt);
                window.Display();

                double sleeping_time = current_time + dt - clock.ElapsedTime.AsSeconds();
                if (sleeping_time > 0)
                {
                    System.Threading.Thread.Sleep((int)(sleeping_time * 100));
                }
            }
        }
    }
}