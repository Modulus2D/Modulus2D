using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Input
{
    public class InputValue
    {
        private float value = 0f;

        public float Value { get => value; set => this.value = value; }

        private Dictionary<Keyboard.Key, Button> keys = new Dictionary<Keyboard.Key, Button>();
        private Dictionary<uint, Button> joystickButtons = new Dictionary<uint, Button>();

        public void OnKeyPressed(Keyboard.Key key)
        {
            if (keys.TryGetValue(key, out Button pair))
            {
                pair.active = true;
            }

            Recalculate();
        }

        public void OnKeyReleased(Keyboard.Key key)
        {
            if (keys.TryGetValue(key, out Button pair))
            {
                pair.active = false;
            }

            Recalculate();
        }

        public void OnJoystickButtonPressed(uint button)
        {
            if (joystickButtons.TryGetValue(button, out Button pair))
            {
                pair.active = true;
            }

            Recalculate();
        }

        public void OnJoystickButtonReleased(uint button)
        {
            if (joystickButtons.TryGetValue(button, out Button pair))
            {
                pair.active = false;
            }

            Recalculate();
        }

        public void AddKey(Keyboard.Key key, float effect)
        {
            Button pair = new Button()
            {
                effect = effect
            };

            keys.Add(key, pair);
        }

        public void RemoveKey(Keyboard.Key key)
        {
            keys.Remove(key);
        }

        public void AddGamepadButton(uint button, float effect)
        {
            Button pair = new Button()
            {
                effect = effect
            };

            joystickButtons.Add(button, pair);
        }

        public void RemoveGamepadButton(uint button)
        {
            joystickButtons.Remove(button);
        }

        private void Recalculate()
        {
            Value = 0f;

            // Calculate keyboard
            foreach (Button button in keys.Values)
            {
                if (button.active)
                {
                    Value += button.effect;
                }
            }

            // Calculate joystick buttons
            foreach (Button button in joystickButtons.Values)
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

    class Button
    {
        public float effect;
        public bool active = false;
    }
}
