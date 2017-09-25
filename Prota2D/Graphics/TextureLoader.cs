using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Graphics
{
    public class TextureLoader
    {
        public Texture Load(string file)
        {
            return new Texture()
            {
                texture = new SFML.Graphics.Texture(file)
            }; ;
        }
    }
}
