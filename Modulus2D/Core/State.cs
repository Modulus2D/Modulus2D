using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Modulus2D.Entities;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Core
{
    public class State
    {
        private RenderTarget target;

        public RenderTarget Target { get => target; set => target = value; }

        public void Load(RenderTarget target)
        {
            Target = target;

            Start();
        }

        public virtual void Start()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }
    }
}
