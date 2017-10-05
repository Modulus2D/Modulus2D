using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Core
{
    public class Config
    {
        private string name;
        private uint width;
        private uint height;

        public Config()
        {
        }

        public Config(string name, uint width, uint height)
        {
            Name = name;
            Width = width;
            Height = height;
        }

        public string Name { get => name; set => name = value; }
        public uint Width { get => width; set => width = value; }
        public uint Height { get => height; set => height = value; }
    }
}
