using NLog;
using OpenGL;
using SFML;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    /// <summary>
    /// A structure for handling a window and an OpenGL context
    /// </summary>
    public class Window : ITarget
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private SFML.Window.Window window;
        private Color clearColor;

        private bool open;

        public int Width => (int)window.Size.X;

        public int Height => (int)window.Size.Y;

        public Color ClearColor {
            get => clearColor;
            set
            {
                clearColor = value;
                Gl.ClearColor(clearColor.Red, clearColor.Green, clearColor.Blue, clearColor.Alpha);
            }
        }

        public bool Open { get => open; set => open = value; }

        public Window(string name, int width, int height)
        {
            // Initialize OpenGL
            Gl.Initialize();

            // Create window
            window = new SFML.Window.Window(new VideoMode((uint)width, (uint)height), name);
            window.SetActive(true);

            Open = true;

            window.Closed += (sender, args) =>
            {
                Open = false;
            };
        }

        public void Dispatch()
        {
            window.DispatchEvents();
            Joystick.Update();
        }

        public void Clear()
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void Swap()
        {
            window.Display();
        }

        public void SetShader(Shader shader)
        {
            shader.Bind();
        }

        public void Draw(VertexArray array)
        {
            array.Bind();
            Gl.DrawArrays(PrimitiveType.Triangles, array.First, array.Vertices.Length);
        }

        public void Draw(VertexArray array, uint[] indices, int count)
        {
            array.Bind();
            Gl.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, indices);
        }
    }
}
