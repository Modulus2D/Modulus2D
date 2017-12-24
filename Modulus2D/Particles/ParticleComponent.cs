using Modulus2D.Entities;
using Modulus2D.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Particles
{
    public class ParticleComponent : IComponent
    {
        private Vector2 position;
        private Vector2 velocity;
        private float timer;

        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public float Timer { get => timer; set => timer = value; }
    }
}
