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

        /// <summary>
        /// Executed when the system is added to the world. Use this to initialize entity filters
        /// </summary>
        public virtual void OnAdded()
        {

        }

        /// <summary>
        /// Use this to update entitites
        /// </summary>
        /// <param name="deltaTime">The time taken to complete the last frame</param>
        public virtual void Update(float deltaTime)
        {
        }

        /// <summary>
        /// Use this to render entitites
        /// </summary>
        /// <param name="deltaTime">The time taken to complete the last frame</param>
        public virtual void Render(float deltaTime)
        {
        }
    }
}
