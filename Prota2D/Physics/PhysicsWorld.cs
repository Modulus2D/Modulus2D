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
        public World world = new World(new Microsoft.Xna.Framework.Vector2(0f, 9.8f));
        private float stepTime = 1 / 60f;
        private float accumulator = 0f;

        public float StepTime { get => stepTime; set => stepTime = value; }

        public void SetGravity(Vector2 gravity)
        {
            world.Gravity.X = gravity.X;
            world.Gravity.Y = gravity.Y;
        }

        public void Update(float dt)
        {
            accumulator += dt;

            while (accumulator >= stepTime)
            {
                world.Step(stepTime);
                accumulator -= stepTime;
            }
        }
    }
}
