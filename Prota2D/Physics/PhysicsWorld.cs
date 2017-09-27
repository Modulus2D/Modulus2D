using FarseerPhysics.Dynamics;
using Prota2D.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class PhysicsWorld
    {
        private World world = new World(new Microsoft.Xna.Framework.Vector2(0f, 9.8f));
        private float deltaTime = 1 / 60f;

        public float DeltaTime { get => deltaTime; set => deltaTime = value; }

        public void SetGravity(Vector2 gravity)
        {
            world.Gravity.X = gravity.X;
            world.Gravity.Y = gravity.Y;
        }

        public void Update()
        {
            world.Step(deltaTime);
        }
    }
}
