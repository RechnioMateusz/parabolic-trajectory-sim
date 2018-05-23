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
    /// <summary>
    /// Abstract loop updating window
    /// </summary>
    abstract class MainLoop
    {
        protected RenderWindow window;
        protected Color background_color;
        protected uint width, height;
        protected const double target_fps = 60;
        protected const double dt = 1 / target_fps;
        protected Adapter adapter = new Adapter();

        /// <summary>
        /// Constructor creating new window instance
        /// and key event handlers
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="title"></param>
        public MainLoop(uint width, uint height, string title)
        {
            this.width = width;
            this.height = height;
            window = new RenderWindow(new VideoMode(this.width, this.height), title, Styles.Titlebar, new ContextSettings { DepthBits = 8, AntialiasingLevel = 2 });
            this.background_color = Color.Black;

            window.KeyPressed += Window_KeyPressed;
            window.KeyReleased += Window_KeyReleased;
            window.MouseButtonPressed += Window_MouseButtonPressed;
            window.MouseButtonReleased += Window_MouseButtonReleased;
            window.MouseWheelMoved += Window_MouseWheelMoved;
            window.MouseMoved += Window_MouseMoved;
        }

        /// <summary>
        /// Loads content before loop start
        /// </summary>
        protected abstract void LoadContent();

        /// <summary>
        /// Initialize loaded content before loop start
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Updates logic
        /// </summary>
        /// <param name="dt"></param>
        protected abstract void Update(double dt);

        /// <summary>
        /// Updates UI
        /// </summary>
        /// <param name="leftover_time"></param>
        protected abstract void Render(double leftover_time);

        /// <summary>
        /// Mouse move event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            adapter.cursor.x = e.X;
            adapter.cursor.y = e.Y;
        }

        /// <summary>
        /// Mouse buttons pressed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:
                    adapter.LMB_click = true;
                    break;
                case Mouse.Button.Right:
                    adapter.RMB_click = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Mouse buttons released event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:
                    adapter.LMB_click = false;
                    break;
                case Mouse.Button.Right:
                    adapter.RMB_click = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Mouse wheel moved event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            adapter.wheel_moved = e.Delta;
        }

        /// <summary>
        /// Keyboard key pressed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.F1:
                    adapter.vectors *= -1;
                    break;
                case Keyboard.Key.F2:
                    adapter.fill *= -1;
                    break;
                case Keyboard.Key.C:
                    adapter.clear = true;
                    break;
                case Keyboard.Key.Escape:
                    window.Close();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Keyboard key released event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.C:
                    adapter.clear = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Main Loop
        /// </summary>
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