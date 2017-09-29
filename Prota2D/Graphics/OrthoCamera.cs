using Prota2D.Math;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Graphics
{
    public class OrthoCamera
    {
        private View view;

        private Vector2 position;
        public Vector2 Position { get => position; set => position = value; }

        private float zoom;
        public float Zoom { get => zoom; set => zoom = value; }
        
        public OrthoCamera(float size)
        {
            float width = size;

            /*view.
            view = new View(new FloatRect(-width / 2f, -height / 2f,));*/
        }

        public void Resize()
        {

        }
    }
}
