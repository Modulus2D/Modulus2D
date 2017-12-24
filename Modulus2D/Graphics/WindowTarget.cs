using OpenGL;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modulus2D.Graphics.Core;

namespace Modulus2D.Graphics
{
    class WindowTarget : IRenderTarget
    {
        private Color clearColor;

        public WindowTarget(Window window)
        {
            Gl.Viewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            window.Resized += new EventHandler<SizeEventArgs>((sender, args) =>
            {
                Gl.Viewport(0, 0, (int)args.Width, (int)args.Height);
            });
        }

        public void Clear(Color color)
        {
            if (clearColor != color)
            {
                clearColor = color;
                
                Gl.ClearColor(clearColor.Red, clearColor.Green, clearColor.Blue, clearColor.Alpha);
            }

            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void Draw(Mesh mesh)
        {
            Gl.EnableVertexAttribArray(0);

            Gl.BindBuffer(BufferTarget.ArrayBuffer, mesh.vao);
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Gl.DisableVertexAttribArray(0);
        }

        public void Draw(IndexedMesh mesh)
        {
        }

        public void SetShader(Shader shader)
        {
            shader.Bind();
        }
    }
}
