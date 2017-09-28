using Prota2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public abstract class EntitySystem
    {
        public virtual void Init(EntityWorld world)
        {

        }

        public virtual void Update(EntityWorld world, float deltaTime)
        {

        }
    }
}
