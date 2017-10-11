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

        private Dictionary<Keyboard.Key, Pair> keys = new Dictionary<Keyboard.Key, Pair>();

        public void OnKeyPressed(Keyboard.Key key)
        {
            if (keys.TryGetValue(key, out Pair pair))
            {
                pair.active = true;
            }

            CalculateValue();
        }

        public void OnKeyReleased(Keyboard.Key key)
        {
            if (keys.TryGetValue(key, out Pair pair))
            {
                pair.active = false;
            }

            CalculateValue();
        }

        public void AddKey(Keyboard.Key key, float effect)
        {
            Pair pair = new Pair()
            {
                effect = effect
            };

            keys.Add(key, pair);
        }

        public void RemoveKey(Keyboard.Key key)
        {
            keys.Remove(key);
        }

        private void CalculateValue()
        {
            Value = 0f;

            foreach (Pair pair in keys.Values)
            {
                if (pair.active)
                {
                    Value += pair.effect;
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

    class Pair
    {
        public float effect;
        public bool active = false;
    }
}
