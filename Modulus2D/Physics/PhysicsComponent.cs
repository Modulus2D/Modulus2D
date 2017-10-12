using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Modulus2D.Entities;
using Modulus2D.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Physics
{
    public class PhysicsComponent : IComponent, ITransmit, IReceive
    {
        private float lerpDistance = 0.3f;
        private float lerp = 0.4f;
        private float snapDistance = 2f;

        /// <summary>
        /// Distance at which a body's position is lerped
        /// </summary>
        public float LerpDistance { get => lerpDistance; set => lerpDistance = value; }

        /// <summary>
        /// Amount of linear interpolation
        /// </summary>
        public float Lerp { get => lerp; set => lerp = value; }

        /// <summary>
        /// Distance at which a body's position is snapped
        /// </summary>
        public float SnapDistance { get => snapDistance; set => snapDistance = value; }

        private Body body;
        public Body Body { get => body; set => body = value; }

        public void Init(World world)
        {
            Body = BodyFactory.CreateBody(world);
            Body.BodyType = BodyType.Dynamic;
        }

        public Fixture CreateBox(float width, float height, float density)
        {
            Shape shape = new PolygonShape(PolygonTools.CreateRectangle(width / 2f, height / 2f), density);
            return Body.CreateFixture(shape);
        }

        public Fixture CreateCircle(float radius, float density)
        {
            Shape shape = new CircleShape(radius, density);
            return Body.CreateFixture(shape);
        }

        public IUpdate Send()
        {
            return new PhysicsUpdate()
            {
                x = Body.Position.X,
                y = Body.Position.Y,
                xVel = Body.LinearVelocity.X,
                yVel = Body.LinearVelocity.Y
            };
        }

        public void Receive(IUpdate update)
        {
            PhysicsUpdate physicsUpdate = (PhysicsUpdate)update;

            Vector2 truePosition = new Vector2(physicsUpdate.x, physicsUpdate.y);
            float diff = Vector2.Distance(Body.Position, truePosition);

            if (diff > LerpDistance)
            {
                if (diff > SnapDistance)
                {
                    Body.Position = truePosition;
                } else
                {
                    Body.Position = Vector2.Lerp(Body.Position, truePosition, Lerp);
                }
            }

            // Snap velocity
            Body.LinearVelocity = new Vector2(physicsUpdate.xVel, physicsUpdate.yVel);
        }
    }
    
    [Serializable]
    class PhysicsUpdate : IUpdate
    {
        public float x = 0f;
        public float y = 0f;

        public float xVel = 0f;
        public float yVel = 0f;
    }
}
