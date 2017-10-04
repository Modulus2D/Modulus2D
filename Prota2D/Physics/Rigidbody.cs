using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Prota2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Physics
{
    public class Rigidbody : IComponent
    {
        private Body body;
        public Body Body { get => body; set => body = value; }

        public void Init(World world)
        {
            Body = BodyFactory.CreateBody(world);
            Body.BodyType = BodyType.Dynamic;
        }
    }
}
