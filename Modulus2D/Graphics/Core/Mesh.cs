using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace Modulus2D.Graphics.Core
{
    public class Mesh
    {
        //TODO: Make private
        public uint vao;
        public uint vbo;

        public Mesh()
        {
            // Generate vertex array object
            vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);

            // Generate buffer
            vbo = Gl.GenBuffer();

            float[] vertices = {
        -0.5f,  0.5f, 1.0f, 0.0f, 0.0f, // Top-left
         0.5f,  0.5f, 0.0f, 1.0f, 0.0f, // Top-right
         0.5f, -0.5f, 0.0f, 0.0f, 1.0f, // Bottom-right
        -0.5f, -0.5f, 1.0f, 1.0f, 1.0f  // Bottom-left
            };

            Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)vertices.Length * sizeof(float), vertices, BufferUsage.StaticDraw);

            // EBO
            uint ebo = Gl.GenBuffer();

            uint[] elements =
            {
                0, 1, 2,
                2, 3, 0
            };

            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)elements.Length * sizeof(uint), elements, BufferUsage.StaticDraw);
        }

        public void SetVertices()
        {

        }
    }

    public class IndexedMesh : Mesh
    {

    }
    /*

    public class InstancedMesh : Mesh
    {

    }*/
}
