using Modulus2D.Input;
using System.Diagnostics;
using NLog;
using Modulus2D.Graphics;
using Modulus2D.Resources;
using System;
using Modulus2D.Math;

namespace Modulus2D.Core
{
    public class Game
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // Current state
        private State state;

        // Input
        private InputManager input;

        private ResourceManager resourceManager;

        // Window
        // private RenderWindow window;

        // Time
        private Stopwatch stopwatch;

        public Game()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();

            input = new InputManager();
        }

        public State State
        {
            get => state;
            set
            {
                state = value;
                // state.Graphics = window;
                state.Input = input;
            }
        }

        public void Start(State state, string name, int width, int height)
        {
            /*// Add input handlers
            window.KeyPressed += new EventHandler<KeyEventArgs>(input.OnKeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(input.OnKeyReleased);
            window.JoystickButtonPressed += new EventHandler<JoystickButtonEventArgs>(input.OnJoystickButtonPressed);
            window.JoystickButtonReleased += new EventHandler<JoystickButtonEventArgs>(input.OnJoystickButtonReleased);*/

            // Set state
            State = state;
            state.Start();

            resourceManager = new ResourceManager();

            Window window = new Window(name, width, height)
            {
                ClearColor = new Color(0.1f, 0.1f, 0.1f)
            };

            State = state;
            state.Start();

            SpriteBatch batch = new SpriteBatch(window);
            
            Texture texture = new Texture("Textures/Wheel.png");

            while (window.Open)
            {
                window.Dispatch();
                
                window.Clear();

                batch.Draw(texture, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0f, 0f), new Vector2(1f, 1f), 0f);
                batch.End();

                Update();

                window.Swap();
            }

            state.Close();
        }

        public void Update()
        {
            float dt = (float)stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();

            // Update state
            State.Update(dt);
        }
    }
}
