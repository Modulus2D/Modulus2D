using Modulus2D.Entities;
using SFML.Graphics;

namespace Modulus2D.Graphics
{
    public class SpriteRenderer : IComponent
    {
        private Texture texture;
        public Texture Texture { get => texture; set => texture = value; }

        public SpriteRenderer(Texture texture)
        {
            Texture = texture;
        }
    }
}

