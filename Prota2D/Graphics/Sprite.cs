using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Entities;

namespace Prota2D.Graphics
{
    public class Sprite : IComponent
    {
        public SFML.Graphics.Sprite sprite;

        public Sprite(string file)
        {
            sprite.Texture = new SFML.Graphics.Texture(file);
        } 
    }
}

