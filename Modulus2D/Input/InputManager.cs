using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Input
{
    public static class Xbox
    {
        public const uint A = 0;
        public const uint B = 1;
        public const uint X = 2;
        public const uint Y = 3;
        public const uint LeftButton = 4;
        public const uint RightButton = 5;
        public const uint LeftTrigger = 6;
        public const uint RightTrigger = 7;
        public const uint Back = 8;
        public const uint Select = 9;
        public const uint LeftJoystick = 10;
        public const uint RightJoystick = 11;
        public const uint Up = 12;
        public const uint Down = 13;
        public const uint Left = 14;
        public const uint Right = 15;
    }

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

        public void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs e)
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i].OnJoystickButtonPressed(e.Button);
            }
        }

        public void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs e)
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i].OnJoystickButtonReleased(e.Button);
            }
        }
    }
}
