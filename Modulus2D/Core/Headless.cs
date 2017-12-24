using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Modulus2D.Core
{
    public class Headless
    {        
        // Current state
        private State state;

        // Time
        private Stopwatch stopwatch;

        public Headless()
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
            }
        }

        public void Start(State state)
        {
            // Set state
            State = state;
            state.Start();

            // Game loop
            while (true)
            {
                float dt = (float)stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();

                State.Update(dt);
            }
        }

        public void Update()
        {
        }
    }
}
