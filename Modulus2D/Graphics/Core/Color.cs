using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics.Core
{
    /// <summary>
    /// A class for color operations
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
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        /// <summary>
        /// Linearly interpolate between two colors
        /// </summary>
        public static Color Lerp(Color a, Color b)
        {
            return new Color((a.Red + b.Red) / 2f, (a.Green + b.Green) / 2f, (a.Blue + b.Blue) / 2f, (a.Alpha + b.Alpha) / 2f);
        }
    }
}
