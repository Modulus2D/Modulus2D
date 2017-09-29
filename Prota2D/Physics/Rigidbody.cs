using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Prota2D.Entities;
using Prota2D.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Physics
{
    public class Rigidbody : IComponent
    {
        internal Body body;

        private Vector2 position = Vector2.Zero;
        private Microsoft.Xna.Framework.Vector2 bodyPos = new Microsoft.Xna.Framework.Vector2();
        public Vector2 Position
        {
            get
            {
                position.X = body.Position.X;
                position.Y = body.Position.Y;
                return position;
            }
            set
            {
                bodyPos.X = value.X;
                bodyPos.Y = value.Y;
                body.Position = bodyPos;
            }
        }

        public bool IsStatic
        {
            get => body.IsStatic;
            set => body.IsStatic = value;
        }

        public void Init(World world)
        {
            body = BodyFactory.CreateBody(world);
            body.BodyType = BodyType.Dynamic;
        }
    }
}
