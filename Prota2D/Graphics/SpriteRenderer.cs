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
            sprite = new SFML.Graphics.Sprite()
            {
                Origin = new SFML.System.Vector2f(0.5f, 0.5f),
                Texture = spriteTexture.texture
            };
        }
    }
}

