using Modulus2D.Input;
using System.Diagnostics;
using NLog;
using Modulus2D.Graphics;
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

        // Window
        private Window window;

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
                state.Graphics = window;
                state.Input = input;
            }
        }

        public void Start(State state, string name, int width, int height)
        {
            // Create window
            window = new Window(name, width, height)
            {
                ClearColor = new Color(0.1f, 0.1f, 0.1f)
            };

            // Set input manager
            window.SetInput(input);

            // Set state
            State = state;
            state.Start();
            
            while (window.Open)
            {
                window.Dispatch();
                
                window.Clear();

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
