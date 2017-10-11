using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Input
{
    public class InputManager
    {
        List<InputValue> values = new List<InputValue>();

        public InputValue Create()
        {
            InputValue value = new InputValue();
            values.Add(value);
            return value;
        }

        public void Remove(InputValue value)
        {
            values.Remove(value);
        }

        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i].OnKeyPressed(e.Code);
            }
        }

        public void OnKeyReleased(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i].OnKeyReleased(e.Code);
            }
        }
    }
}
