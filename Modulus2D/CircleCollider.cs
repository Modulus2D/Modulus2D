using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Physics
{
    public class CircleCollider : IComponent
    {
        private float radius = 1f;
        private float density = 1f;
        private Fixture fixture;

        public CircleCollider(float circleRadius)
        {
            radius = circleRadius;
        }

        public CircleCollider(float circleRadius, float circleDensity)
        {
            radius = circleRadius;
            density = circleDensity;
        }

        public void Init(Body body)
        {
            Shape shape = new CircleShape(radius, density);

            fixture = body.CreateFixture(shape);
        }
    }
}
