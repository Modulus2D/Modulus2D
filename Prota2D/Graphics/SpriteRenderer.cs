using Prota2D.Entities;
using SFML.Graphics;

namespace Prota2D.Graphics
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

