using Modulus2D.Entities;
using SFML.Graphics;

namespace Modulus2D.Graphics
{
    public class SpriteComponent : IComponent
    {
        private Texture texture;
        public Texture Texture { get => texture; set => texture = value; }

        public SpriteComponent(Texture texture)
        {
            Texture = texture;
        }
    }
}

