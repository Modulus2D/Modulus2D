using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class Viewport
    {
        private float width;
        private float height;

        public Viewport(float screenWidth, float screenHeight, float worldWidth)
        {
            width = worldWidth;
            height = worldWidth * screenHeight / screenWidth;
        }

        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }
    }
}
