using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Core
{
    public class Game
    {
        // Current state
        private State state;

        // Window
        private RenderWindow window;

        // Time
        private Clock clock = new Clock();

        public RenderWindow Window { get => window; set => window = value; }

        public State State {
            get => state;
            set {
                state = value;
                state.Target = Window;
                state.Start();
            }
        }

        public void Start(State state, string name, uint width, uint height)
        {
            // Create window
            Window = new RenderWindow(new SFML.Window.VideoMode(width, height), name);
            Window.SetActive();
            Window.Closed += new EventHandler(OnClosed);

            // Set state
            State = state;

            // Render loop
            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                Window.Clear(new Color(0, 0, 0));

                Update();

                Window.Display();
            }
        }

        public void Update()
        {
            float dt = clock.ElapsedTime.AsSeconds();
            clock.Restart();

            // Update state
            State.Update(dt);
        }

        // TODO: Create separate render loop?
        /*public void Render()
        {

        }*/

        static void OnClosed(object sender, EventArgs e)
        {
            SFML.Window.Window window = (SFML.Window.Window)sender;
            window.Close();
        }
    }
}
