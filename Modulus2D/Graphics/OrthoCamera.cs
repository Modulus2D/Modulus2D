using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class OrthoCamera
    {
        private float zoom = 1f;
        public float Zoom { get => zoom; set => zoom = value; }

        private Vector2 position;
        private Vector2f viewPosition;
        public Vector2 Position {
            get
            {
                position.X = View.Center.X;
                position.Y = View.Center.Y;
                return position;
            }
            set {
                viewPosition.X = value.X;
                viewPosition.Y = value.Y;
                View.Center = viewPosition;
            }
        }
        
        private View view;
        public View View { get => view; set => view = value; }

        public OrthoCamera(Viewport viewport)
        {
            view = new View(new FloatRect(0f, 0f, viewport.Width, viewport.Height));
        }
    }
}
