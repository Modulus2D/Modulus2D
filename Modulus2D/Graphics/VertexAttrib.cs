using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class VertexAttrib
    {
        private int size;
        private bool normalized;

        public int Size { get => size; set => size = value; }
        public bool Normalized { get => normalized; set => normalized = value; }
    }
}
