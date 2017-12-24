using Modulus2D.Entities;
using Modulus2D.Math;
using SFML.Graphics;
using System.Collections.Generic;

namespace Modulus2D.Graphics
{
    public class SpriteRendererComponent : IComponent
    {
        public List<Sprite> sprites = new List<Sprite>();

        public SpriteRendererComponent()
        {

        }

        public void AddSprite(SFML.Graphics.Texture texture)
        {
            sprites.Add(new Sprite()
            {
                offset = new Vector2(0f, 0f),
                texture = texture
            });
        }

        public void AddSprite(SFML.Graphics.Texture texture, Vector2 offset)
        {
            sprites.Add(new Sprite()
            {
                offset = offset,
                texture = texture
            });
        }
    }

    public class Sprite
    {
        public Vector2 offset;
        public SFML.Graphics.Texture texture;
    }
}