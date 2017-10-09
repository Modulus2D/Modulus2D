using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Physics
{
    public class BoxCollider : IComponent
    {
        private float width = 1f;
        private float height = 1f;
        private float density = 1f;
        private Fixture fixture;

        public BoxCollider(float rectWidth, float rectHeight)
        {
            width = rectWidth;
            height = rectHeight;
        }

        public BoxCollider(float rectWidth, float rectHeight, float rectDensity)
        {
            width = rectWidth;
            height = rectHeight;
            density = rectDensity;
        }

        public void Init(Body body)
        {
            Shape shape = new PolygonShape(PolygonTools.CreateRectangle(width / 2f, height / 2f), density);

            fixture = body.CreateFixture(shape);
        }
    }
}
