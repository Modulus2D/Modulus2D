using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Core
{
    public class Server
    {        
        // Current state
        private State state;

        // Time
        // TODO: Replace with custom Clock class to remove SFML dependency
        private Clock clock = new Clock();

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
                float dt = clock.ElapsedTime.AsSeconds();
                clock.Restart();
                
                State.Update(dt);

                // Avoid extreme CPU usage
                System.Threading.Thread.Sleep(0);
            }
        }

        public void Update()
        {
        }
    }
}
