using Modulus2D.Entities;
using Modulus2D.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modulus2D.Input;
using Modulus2D.Graphics;

namespace Modulus2D.Core
{
    public class State
    {
        private Window graphics;
        private InputManager input;

        public Window Graphics { get => graphics; set => graphics = value; }
        public InputManager Input { get => input; set => input = value; }

        public virtual void Start()
        {

        }

        public virtual void Close()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }
    }
}
