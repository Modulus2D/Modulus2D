using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Input
{
    public class ContinuousInput : BasicInput
    {
        private float value = 0f;

        public float Value { get => value; set => this.value = value; }

        public override void Recalculate()
        {
            Value = 0f;

            // Calculate keyboard
            foreach (Button button in Keys.Values)
            {
                if (button.active)
                {
                    Value += button.effect;
                }
            }

            // Calculate joystick buttons
            foreach (Button button in JoystickButtons.Values)
            {
                if (button.active)
                {
                    Value += button.effect;
                }
            }

            // Clamp value to range
            if (value > 1f)
            {
                value = 1f;
            }
            else if (value < -1f)
            {
                value = -1f;
            }
        }
    }
}
