using Modulus2D.Entities;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Particles
{
    /// <summary>
    /// A particle system
    /// </summary>
    // TODO: Redesign graphics using OpenGL so that instancing may be used
    public class ParticleSystem : EntitySystem
    {
        private EntityFilter filter;
        private ComponentStorage<ParticleComponent> particleComponents;

        private RenderTarget target;

        private float lifeTime;

        public float LifeTime { get => lifeTime; set => lifeTime = value; }


        public ParticleSystem(RenderTarget target)
        {
            this.target = target;
        }

        public override void OnAdded()
        {
            particleComponents = World.GetStorage<ParticleComponent>();

            filter = new EntityFilter(particleComponents);
        }

        public override void Update(float deltaTime)
        {
            foreach(int id in World.Iterate(filter))
            {
                ParticleComponent particle = particleComponents.Get(id);
            }
        }
    }
}
