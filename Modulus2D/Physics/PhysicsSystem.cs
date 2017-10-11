using FarseerPhysics.Dynamics;
using Modulus2D.Core;
using Modulus2D.Entities;
using Microsoft.Xna.Framework;

namespace Modulus2D.Physics
{
    public class PhysicsSystem : EntitySystem
    {
        private EntityFilter filter = new EntityFilter();
        private World physicsWorld;

        public World PhysicsWorld { get => physicsWorld; set => physicsWorld = value; }
        private float stepTime = 0.015f; // ~66 FPS
        private float accumulator = 0f;

        public PhysicsSystem()
        {
            PhysicsWorld = new World(new Vector2(0f, 9.8f));

            filter.Add<TransformComponent>();
            filter.Add<PhysicsComponent>();
        }

        public override void AddedToWorld()
        {
            World.AddCreatedListener<PhysicsComponent>(Created);
            World.AddRemovedListener<PhysicsComponent>(Destroyed);
        }

        public void Created(Entity entity)
        {
            PhysicsComponent physics = entity.GetComponent<PhysicsComponent>();
            physics.Init(PhysicsWorld);
        }

        public void Destroyed(Entity entity)
        {
            PhysicsComponent physics = entity.GetComponent<PhysicsComponent>();
            PhysicsWorld.RemoveBody(physics.Body);
        }

        public override void Update(float deltaTime)
        {
            // Update world
            accumulator += deltaTime;
            
            while (accumulator >= stepTime)
            {
                PhysicsWorld.Step(stepTime);
                accumulator -= stepTime;
            }
            
            // Update transforms
            foreach (Components components in World.Iterate(filter))
            {
                TransformComponent transform = components.Next<TransformComponent>();
                PhysicsComponent physics = components.Next<PhysicsComponent>();
                
                if (physics.Body != null)
                {
                    // Lerp body position
                    transform.Position += (physics.Body.Position - transform.Position) * accumulator / stepTime;
                    
                    // TODO: Negative rotation?
                    transform.Rotation += (-physics.Body.Rotation - transform.Rotation) * accumulator / stepTime;
                }
            }
        }
    }
}
