using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    public abstract class EntitySystem
    {
        private EntityWorld world;

        public EntityWorld World { get => world; set => world = value; }

        public virtual void Start()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }
    }
}
