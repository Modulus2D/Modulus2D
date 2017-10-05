using Microsoft.Xna.Framework;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Core
{
    public class TransformComponent : IComponent
    {
        private Vector2 position = Vector2.Zero;
        private Vector2 scale = new Vector2(1f, 1f);
        private float rotation = 0f;

        public Vector2 Position {
            get => position;
            set => position = value;
        }
        public Vector2 Scale { get => scale; set => scale = value; }
        public float Rotation { get => rotation; set => rotation = value; }
    }
}
