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
        private float lerpDistance = 0.0f;
        private float positionLerp = 0.005f;
        private float rotationLerp = 0.005f;
        private float snapDistance = 2f;

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

        public Fixture CreateBox(float width, float height, Vector2 position, float density)
        {
            Shape shape = new PolygonShape(PolygonTools.CreateRectangle(width / 2f, height / 2f, position, 0f), density);
            return Body.CreateFixture(shape);
        }

        public Fixture CreateCircle(float radius, float density)
        {
            Shape shape = new CircleShape(radius, density);
            return Body.CreateFixture(shape);
        }

        public IUpdate Send()
        {
            Console.WriteLine("SEND");

            return new PhysicsUpdate()
            {
                x = Body.Position.X,
                y = Body.Position.Y,
                rot = Body.Rotation,
                xVel = Body.LinearVelocity.X,
                yVel = Body.LinearVelocity.Y
            };
        }

        public void Receive(IUpdate update)
        {
            Console.WriteLine("RECE");

            PhysicsUpdate physicsUpdate = (PhysicsUpdate)update;

            Vector2 truePosition = new Vector2(physicsUpdate.x, physicsUpdate.y);
            float diff = Vector2.Distance(Body.Position, truePosition);
            
            if (diff > snapDistance)
            {
                //Body.Position = truePosition;

                //Console.WriteLine("Snap");
            } else
            {
                // Body.Position = Vector2.Lerp(Body.Position, truePosition, rotationLerp);

                // Console.WriteLine("Lerp");
            }

            // Snap rotation
            //Body.Rotation += rotationLerp * (physicsUpdate.rot - Body.Rotation);

            // Snap velocity
            //Body.LinearVelocity = new Vector2(physicsUpdate.xVel, physicsUpdate.yVel);
        }
    }
    
    [Serializable]
    class PhysicsUpdate : IUpdate
    {
        public float x = 0f;
        public float y = 0f;

        public float rot = 0f;

        public float xVel = 0f;
        public float yVel = 0f;
    }
}
