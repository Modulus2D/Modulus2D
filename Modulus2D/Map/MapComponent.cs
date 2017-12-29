using Modulus2D.Entities;
using Modulus2D.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Map
{
    public class MapComponent : IComponent
    {
        private string filename;
        private List<float> vertices;
        private VertexArray vertexArray;

        public MapComponent(string filename)
        {
            Filename = filename;
        }

        public string Filename { get => filename; set => filename = value; }
        public List<float> Vertices { get => vertices; set => vertices = value; }
        public VertexArray VertexArray { get => vertexArray; set => vertexArray = value; }
    }
}
