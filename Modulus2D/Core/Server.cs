using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Modulus2D.Core
{
    public class Server
    {        
        // Current state
        private State state;

        // Time
        private Stopwatch stopwatch;

        public Server()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public State State
        {
            get => state;
            set
            {
                state = value;
                state.Start();
            }
        }

        public void Start(State state)
        {
            // Set state
            State = state;

            // Game loop
            while (true)
            {
                float dt = (float)stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();

                State.Update(dt);

                // Avoid extreme CPU usage
                // System.Threading.Thread.Sleep(0);
            }
        }

        public void Update()
        {
        }
    }
}
