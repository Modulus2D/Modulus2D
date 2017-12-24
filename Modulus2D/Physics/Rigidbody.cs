using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Modulus2D.Entities;
using Modulus2D.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Modulus2D.Math;
using FarseerPhysics.Dynamics.Contacts;

namespace Modulus2D.Physics
{
    public delegate void Collision(Rigidbody bodyA, Rigidbody bodyB);
    public delegate void Separation(Rigidbody bodyA, Rigidbody bodyB);

    public class Rigidbody : IComponent, INetView
    {
        public event Collision Collision;
        public event Separation Separation;

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

        // Getters and setters for internal body

        /// <summary>
        /// Center of mass of the body in world coordinates
        /// </summary>
        public Vector2 WorldCenter { get => Vector2.Convert(body.WorldCenter); }

        /// <summary>
        /// Center of mass of the body in local coordinates
        /// </summary>
        public Vector2 LocalCenter { get => Vector2.Convert(body.LocalCenter); }

        /// <summary>
        /// Amount of linear damping
        /// </summary>
        public float LinearDamping { get => body.LinearDamping; set => body.LinearDamping = value; }

        /// <summary>
        /// Current linear velocity
        /// </summary>
        public Vector2 LinearVelocity { get => Vector2.Convert(body.LinearVelocity); set => body.LinearVelocity = Vector2.Convert(value); }

        /// <summary>
        /// Amount of angular damping
        /// </summary>
        public float AngularDamping { get => body.AngularDamping; set => body.AngularDamping = value; }

        /// <summary>
        /// Current angular velocity
        /// </summary>
        public float AngularVelocity { get => body.AngularVelocity; set => body.AngularVelocity = value; }

        /// <summary>
        /// Current position
        /// </summary>
        public Vector2 Position { get => Vector2.Convert(body.Position); set => body.Position = Vector2.Convert(value); }

        /// <summary>
        /// Current rotation
        /// </summary>
        public float Rotation { get => body.Rotation; set => body.Rotation = value; }

        public bool Awake { get => body.Awake; set => body.Awake = value; }
        
        /// <summary>
        /// Body type
        /// </summary>
        public BodyType Type { get => (BodyType)body.BodyType; set => body.BodyType = (FarseerPhysics.Dynamics.BodyType)value; }
        
        public bool FixedRotation { get => body.FixedRotation; set => body.FixedRotation = value; }

        public float Friction { get => body.Friction; set => body.Friction = value; }

        public float GravityScale { get => body.GravityScale; set => body.GravityScale = value; }

        public bool IgnoreGravity { get => body.IgnoreGravity; set => body.IgnoreGravity = value; }

        public float Inertia { get => body.Inertia; set => body.Inertia = value; }

        public bool InWorld { get => body.InWorld; }

        public bool IsBullet { get => body.IsBullet; set => body.IsBullet = value; }

        public bool IsSensor { set => body.IsSensor = value; }

        public bool IsStatic { get => body.IsStatic; set => body.IsStatic = value; }

        public float Mass { get => body.Mass; set => body.Mass = value; }

        public float Restitution { get => body.Restitution; set => body.Restitution = value; }

        public void Init(World world)
        {
            body = BodyFactory.CreateBody(world);
            
            Type = BodyType.Dynamic;

            body.OnCollision += OnCollision;
            body.OnSeparation += OnSeparation;
        }

        private bool OnCollision(Fixture a, Fixture b, Contact contact)
        {
            if (b.UserData is Rigidbody rb)
            {
                Collision(this, rb);
            }

            return false;
        }

        private void OnSeparation(Fixture a, Fixture b)
        {
            if (b.UserData is Rigidbody rb)
            {
                Separation(this, rb);
            }
        }

        public Fixture CreateBox(float width, float height, float density)
        {
            Shape shape = new PolygonShape(PolygonTools.CreateRectangle(width / 2f, height / 2f), density);
            return CreateFixture(shape);
        }

        public Fixture CreateBox(float width, float height, Vector2 position, float density, float angle = 0f)
        {
            Shape shape = new PolygonShape(PolygonTools.CreateRectangle(width / 2f, height / 2f, new Microsoft.Xna.Framework.Vector2(position.X, position.Y), angle), density);
            return CreateFixture(shape);
        }

        public Fixture CreateCircle(float radius, float density)
        {
            Shape shape = new CircleShape(radius, density);
            return CreateFixture(shape);
        }

        private Fixture CreateFixture(Shape shape)
        {
            return body.CreateFixture(shape, this);
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
            buffer.Write(Position.X);
            buffer.Write(Position.Y);
            buffer.Write(Rotation);
        }
    }
}