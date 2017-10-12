using Modulus2D.Core;
using Modulus2D.Input;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
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

        // Input
        private InputManager input = new InputManager();

        // Time
        private Clock clock = new Clock();

        public State State {
            get => state;
            set {
                state = value;
                state.Graphics = window;
                state.Input = input;
                state.Start();
            }
        }

        public void Start(State state, string name, uint width, uint height)
        {
            // Create window
            window = new RenderWindow(new VideoMode(width, height), name);
            window.SetActive();
            window.Closed += new EventHandler(OnClosed);
            
            // Add input handlers
            window.KeyPressed += new EventHandler<KeyEventArgs>(input.OnKeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(input.OnKeyReleased);
            window.JoystickButtonPressed += new EventHandler<JoystickButtonEventArgs>(input.OnJoystickButtonPressed);
            window.JoystickButtonReleased += new EventHandler<JoystickButtonEventArgs>(input.OnJoystickButtonReleased);

            // Set state
            State = state;

            // Render loop
            while (window.IsOpen)
            {
                window.DispatchEvents();
                Joystick.Update();

                window.Clear(new Color(0, 0, 0));

                Update();

                window.Display();
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

        public void OnClosed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }
    }
}
