using Modulus2D.Entities;
using SFML.Graphics;
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
        private VertexArray vertices;
        private RenderStates states;

        public MapComponent(string filename)
        {
            Filename = filename;
        }

        public string Filename { get => filename; set => filename = value; }
        public VertexArray Vertices { get => vertices; set => vertices = value; }
        public RenderStates States { get => states; set => states = value; }
    }
}
