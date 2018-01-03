using Modulus2D.Entities;
using Modulus2D.Math;
using SFML.Graphics;
using System.Collections.Generic;

namespace Modulus2D.Graphics
{
    public class SpriteComponent : IComponent
    {
        private Texture texture;
        private Vector2 offset;

        public Texture Texture { get => texture; set => texture = value; }
        public Vector2 Offset { get => offset; set => offset = value; }

        public SpriteComponent(Texture texture)
        {
            Texture = texture;
            Offset = Vector2.Zero;
        }
    }
}