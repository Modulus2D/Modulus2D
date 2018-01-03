using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Input
{
    public class BasicInput
    {
        private Dictionary<Keyboard.Key, Button> keys = new Dictionary<Keyboard.Key, Button>();
        private Dictionary<uint, Button> joystickButtons = new Dictionary<uint, Button>();

        internal Dictionary<Keyboard.Key, Button> Keys { get => keys; set => keys = value; }
        internal Dictionary<uint, Button> JoystickButtons { get => joystickButtons; set => joystickButtons = value; }

        public void OnKeyPressed(Keyboard.Key key)
        {
            if (Keys.TryGetValue(key, out Button pair))
            {
                pair.active = true;

                Recalculate();
            }
        }

        public void OnKeyReleased(Keyboard.Key key)
        {
            if (Keys.TryGetValue(key, out Button pair))
            {
                pair.active = false;

                Recalculate();
            }
        }

        public void OnJoystickButtonPressed(uint button)
        {
            if (JoystickButtons.TryGetValue(button, out Button pair))
            {
                pair.active = true;

                Recalculate();
            }
        }

        public void OnJoystickButtonReleased(uint button)
        {
            if (JoystickButtons.TryGetValue(button, out Button pair))
            {
                pair.active = false;

                Recalculate();
            }
        }

        public void AddKey(Keyboard.Key key, float effect)
        {
            Button pair = new Button()
            {
                effect = effect
            };

            Keys.Add(key, pair);
        }

        public void RemoveKey(Keyboard.Key key)
        {
            Keys.Remove(key);
        }

        public void AddGamepadButton(uint button, float effect)
        {
            Button pair = new Button()
            {
                effect = effect
            };

            JoystickButtons.Add(button, pair);
        }

        public void RemoveGamepadButton(uint button)
        {
            JoystickButtons.Remove(button);
        }

        public virtual void Recalculate() { }
    }

    class Button
    {
        public float effect;
        public bool active = false;
    }
}
