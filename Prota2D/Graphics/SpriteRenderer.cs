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
        internal SFML.Graphics.Sprite sprite;

        private Texture texture;
        public Texture Texture { get => texture; set => texture = value; }

        public SpriteRenderer(Texture spriteTexture)
        {
            sprite = new SFML.Graphics.Sprite();
            sprite.Origin = new SFML.System.Vector2f(0.5f, 0.5f);

            sprite.Texture = spriteTexture.texture;
            //sprite.Texture = new SFML.Graphics.Texture("Textures/Face.png");
            /*Texture = spriteTexture;
            sprite.Texture = Texture.texture;*/
        }
    }
}

