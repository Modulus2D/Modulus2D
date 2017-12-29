using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Modulus2D.Entities;
using Modulus2D.Network;
using Lidgren.Network;
using Modulus2D.Math;

namespace Modulus2D.Physics
{
    public class PhysicsComponent : IComponent, INetView
    {
        public enum BodyType
        {
            Static = 0,
            Kinematic = 1,
            Dynamic = 2,
        }

        // TODO: Move these to a NetworkedRigidbody component?
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
        public NetMode Mode { get => mode; set => mode = value; }

        private Vector2 lastPosition;
        private Vector2 correctPosition;
        private float lastRotation;
        private float correctRotation;

        public Vector2 LastPosition { get => lastPosition; set => lastPosition = value; }
        public Vector2 CorrectPosition { get => correctPosition; set => correctPosition = value; }
        public float LastRotation { get => lastRotation; set => lastRotation = value; }
        public float CorrectRotation { get => correctRotation; set => correctRotation = value; }

        private Body body;
        public Body Body { get => body; set => body = value; }

        public void Init(World world)
        {
            Body = BodyFactory.CreateBody(world);
            Body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
        }

        public Fixture CreateBox(float width, float height, float density)
        {
            Shape shape = new PolygonShape(PolygonTools.CreateRectangle(width / 2f, height / 2f), density);
            return CreateFixture(shape);
        }

        public Fixture CreateBox(float width, float height, Vector2 position, float density, float angle = 0f)
        {
            Shape shape = new PolygonShape(PolygonTools.CreateRectangle(width / 2f, height / 2f, new Vector2(position.X, position.Y), angle), density);
            return CreateFixture(shape);
        }

        public Fixture CreateCircle(float radius, float density)
        {
            Shape shape = new CircleShape(radius, density);
            return CreateFixture(shape);
        }

        private Fixture CreateFixture(Shape shape)
        {
            return Body.CreateFixture(shape, this);
        }

        public void Receive(NetBuffer buffer)
        {
            LastPosition = CorrectPosition;
            CorrectPosition = new Vector2(buffer.ReadFloat(), buffer.ReadFloat());

            LastRotation = CorrectRotation;
            CorrectRotation = buffer.ReadFloat();
        }

        public void Transmit(NetBuffer buffer)
        {
            buffer.Write(Body.Position.X);
            buffer.Write(Body.Position.Y);
            buffer.Write(Body.Rotation);
        }
    }
}