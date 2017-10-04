using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class Window
    {
        private RenderWindow window;

        public float Width { get => RenderWindow.Size.X; }
        public float Height { get => RenderWindow.Size.Y; }
        public float AspectRatio { get => RenderWindow.Size.X / RenderWindow.Size.Y; }
        public RenderWindow RenderWindow { get => window; set => window = value; }
        public OrthoCamera Camera { get => camera; set => camera = value; }

        private OrthoCamera camera;

        public void SetCamera(OrthoCamera orthoCamera)
        {
            RenderWindow.SetView(orthoCamera.View);
            Camera = orthoCamera;
        }

        public Window(RenderWindow renderWindow)
        {
            RenderWindow = renderWindow;
        }
    }
}
