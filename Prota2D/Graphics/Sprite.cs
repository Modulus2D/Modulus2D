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
            sprite = new SFML.Graphics.Sprite();
            sprite.Origin = new SFML.System.Vector2f(0.5f, 0.5f);
            sprite.Texture = new SFML.Graphics.Texture(file);
        } 
    }
}

