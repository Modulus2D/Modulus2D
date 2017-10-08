using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Modulus2D.Entities;
using Prota2D.Core;
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
        private RenderWindow graphics;
        private Input input;

        public RenderWindow Graphics { get => graphics; set => graphics = value; }
        public Input Input { get => input; set => input = value; }

        public virtual void Start()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }
    }
}
