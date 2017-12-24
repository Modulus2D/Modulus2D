using Modulus2D.Math;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.UI
{
    public enum Alignment
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        CenterCenter,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
    }

    public class Widget
    {
        private Vector2 position;
        private Alignment alignment; 

        public Vector2 Position { get => position; set => position = value; }
        public Alignment Alignment { get => alignment; set => alignment = value; }

        public virtual void Draw(RenderTarget target)
        {

        }
    }
}
