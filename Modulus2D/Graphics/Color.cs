using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    /// <summary>
    /// A class for color manipulation
    /// </summary>
    public class Color
    {
        private float red;
        private float green;
        private float blue;
        private float alpha;

        public float Red { get => red; set => red = value; }
        public float Green { get => green; set => green = value; }
        public float Blue { get => blue; set => blue = value; }
        public float Alpha { get => alpha; set => alpha = value; }

        public Color(float red, float green, float blue, float alpha)
        {
            Red = red;
            Blue = blue;
            Green = green;
            Alpha = alpha;
        }

        public Color(float red, float green, float blue)
        {
            Red = red;
            Blue = blue;
            Green = green;
            Alpha = 1.0f;
        }
    }
}
