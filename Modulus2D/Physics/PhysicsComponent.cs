using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Physics
{
    public class PhysicsComponent : IComponent
    {
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
    }
}
