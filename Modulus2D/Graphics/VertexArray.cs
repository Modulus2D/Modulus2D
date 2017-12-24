using OpenGL;
using System;

namespace Modulus2D.Graphics
{
    public class VertexArray
    {
        private uint vbo;
        protected uint vao;

        private VertexAttrib[] attribs;

        private float[] vertices;

        public VertexAttrib[] Attribs
        {
            get => attribs;
            set
            {
                attribs = value;

                // Number of bytes between attributes sets
                int stride = 0;

                for (int i = 0; i < attribs.Length; i++)
                {
                    stride += attribs[i].Size * sizeof(float);
                }

                // Now set attributes
                Bind();

                int ptr = 0;

                for (uint i = 0; i < attribs.Length; i++)
                {
                    Gl.EnableVertexAttribArray(i);

                    Gl.VertexAttribPointer(i, attribs[i].Size, VertexAttribType.Float, attribs[i].Normalized, stride, (IntPtr)ptr);

                    ptr += attribs[i].Size * sizeof(float);
                }
            }
        }

        // TODO: Make this accessible?
        protected BufferUsage usage = BufferUsage.StaticDraw;

        private int first;
        public int First { get => first; set => first = value; }

        /// <summary>
        /// Setting this will update the internal vertex buffer object
        /// </summary>
        public float[] Vertices {
            get => vertices;
            set
            {
                vertices = value;

                Upload();
            }
        }

        public VertexArray()
        {
            Vertices = new float[0];

            // VAO
            vao = Gl.GenVertexArray();
            Bind();
            
            // VBO
            vbo = Gl.GenBuffer();

            // TODO: Removing this causes an access violation when specifying vertex attributes for some reason
            Upload();
        }

        public void Bind()
        {
            Gl.BindVertexArray(vao);
        }

        private void Upload()
        {
            Bind();

            Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            Gl.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * (uint)Vertices.Length, Vertices, usage);
        }
    }
}
