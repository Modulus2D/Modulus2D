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
    public class PhysicsComponent : IComponent, INetComponent
    {
        public enum NetMode
        {
            /// <summary>
            /// Specifies that this entity should be snapped to the raw transform data received from the server
            /// </summary>
            Raw,
            /// <summary>
            /// Specifies that the transform of this entity should be interpolated
            /// </summary>
            Interpolate,
            /// <summary>
            /// Specifies that the transform of this entity should be calculated by the client and corrected
            /// </summary>
            Predict,
            /// <summary>
            /// Specifies that the transform of this entity should be calculated by the client and left uncorrected
            /// </summary>
            Client
        }

        private NetMode mode;
        
        private Body body;
        public Body Body { get => body; set => body = value; }
        public NetMode Mode { get => mode; set => mode = value; }

        // Used to store last frames for interpolation
        internal Vector2 lastPosition;
        internal Vector2 correctPosition;
        internal float lastRotation;
        internal float correctRotation;

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

        public IUpdate Transmit()
        {
            return new PhysicsUpdate()
            {
                X = Body.Position.X,
                Y = Body.Position.Y,
                Rotation = Body.Rotation
            };
        }

        public void Receive(IUpdate update)
        {
            PhysicsUpdate physicsUpdate = (PhysicsUpdate)update;

            lastPosition = correctPosition;
            correctPosition = new Vector2(physicsUpdate.X, physicsUpdate.Y);

            lastRotation = correctRotation;
            correctRotation = physicsUpdate.Rotation;
        }
        
        [Serializable]
        class PhysicsUpdate : IUpdate
        {
            public float X = 0f;
            public float Y = 0f;
            public float Rotation = 0f;
        }
    }
}
