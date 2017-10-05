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

        private Vector2 position = new Vector2(0f, 0f);
        private Vector2f viewPosition = new Vector2f(0f, 0f);
        private View view;

        public Vector2 Position { get => position; set => position = value; }
        public View View { get => view; set => view = value; }

        public OrthoCamera(Viewport viewport)
        {
            view = new View(new FloatRect(0f, 0f, viewport.Width, viewport.Height));
        }

        public void Update()
        {
            viewPosition.X = position.X;
            viewPosition.Y = position.Y;

            view.Center = viewPosition;
        }
    }
}
