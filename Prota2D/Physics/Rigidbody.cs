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
        public Body body;

        public bool IsStatic
        {
            get => body.IsStatic;
            set => body.IsStatic = value;
        }

        public Rigidbody()
        {
            
        }

        public void Init(World world)
        {
            body = BodyFactory.CreateBody(world);
            body.BodyType = BodyType.Dynamic;
        }
    }
}
