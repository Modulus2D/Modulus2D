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
                Update();
            }
        }

        public void Update()
        {
            float dt = clock.ElapsedTime.AsSeconds();
            clock.Restart();

            // Update state
            State.Update(dt);
        }

    }
}
