using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    /// <summary>
    /// Base entity system class
    /// </summary>
    public abstract class EntitySystem
    {
        private int priority = 0;

        private EntityWorld world;
        public EntityWorld World { get => world; set => world = value; }

        /// <summary>
        /// Entity Systems are executed in descending order from highest to lowest priority
        /// </summary>
        public int Priority { get => priority; set => priority = value; }

        public virtual void AddedToWorld()
        {

        }

        public virtual void Update(float deltaTime)
        {
        }
    }
}
