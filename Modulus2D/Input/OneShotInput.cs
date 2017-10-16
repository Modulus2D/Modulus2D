using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Input
{
    public class OneShotInput : BasicInput
    {
        private bool active;

        public bool Active { get => active; set => active = value; }

        public override void Recalculate()
        {
            Active = false;

            // Calculate keyboard
            foreach (Button button in Keys.Values)
            {
                if (button.active)
                {
                    Active = true;
                }
            }

            // Calculate joystick buttons
            foreach (Button button in JoystickButtons.Values)
            {
                if (button.active)
                {
                    Active = true;
                }
            }
        }

        /*public void Reset()
        {
            Active = false;
        }*/
    }
}
