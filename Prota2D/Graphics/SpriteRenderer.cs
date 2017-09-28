using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Entities;

namespace Prota2D.Graphics
{
    public class SpriteRenderer : IComponent
    {
        public SFML.Graphics.Sprite sprite;

        private Texture texture;
        public Texture Texture { get => texture; set => texture = value; }

        public SpriteRenderer(Texture spriteTexture)
        {
            sprite = new SFML.Graphics.Sprite()
            {
                Origin = new SFML.System.Vector2f(0.5f, 0.5f),
                Texture = spriteTexture.texture
            };
        }
    }
}

