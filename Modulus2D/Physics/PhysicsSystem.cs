using FarseerPhysics.Dynamics;
using Modulus2D.Core;
using Modulus2D.Entities;
using Microsoft.Xna.Framework;
using Modulus2D.Network;
using System;
using Lidgren.Network;

namespace Modulus2D.Physics
{
    public class PhysicsSystem : EntitySystem
    {
        private EntityFilter filter;
        private EntityFilter networkFilter;
        private ComponentStorage<TransformComponent> transformComponents;
        private ComponentStorage<PhysicsComponent> physicsComponents;
        private ComponentStorage<NetComponent> netComponents;

        private World physicsWorld;
        private float stepTime = 1 / 60f;
        private float accumulator = 0f;

        public World PhysicsWorld { get => physicsWorld; set => physicsWorld = value; }
        public float StepTime { get => stepTime; set => stepTime = value; }

        public PhysicsSystem()
        {
            PhysicsWorld = new World(new Vector2(0f, 9.8f));
        }

        public override void OnAdded()
        {
            transformComponents = World.GetStorage<TransformComponent>();
            physicsComponents = World.GetStorage<PhysicsComponent>();
            netComponents = World.GetStorage<NetComponent>();

            filter = new EntityFilter(transformComponents, physicsComponents);
            networkFilter = new EntityFilter(physicsComponents, netComponents);

            World.AddCreatedListener<PhysicsComponent>(entity =>
            {
                PhysicsComponent physics = physicsComponents.Get(entity);
                physics.Init(PhysicsWorld);
            });
            World.AddRemovedListener<PhysicsComponent>(entity =>
            {
                PhysicsComponent physics = physicsComponents.Get(entity);
                PhysicsWorld.RemoveBody(physics.Body);
            });
        }

        public override void Update(float deltaTime)
        {
            // Update world
            accumulator += deltaTime;

            while (accumulator >= StepTime)
            {
                PhysicsWorld.Step(StepTime);
                accumulator -= StepTime;
            }

            // Update transforms
            foreach (int id in World.Iterate(filter))
            {
                TransformComponent transform = transformComponents.Get(id);
                PhysicsComponent physics = physicsComponents.Get(id);

                // TODO: Can this be removed?
                if (physics.Body != null)
                {
                    // Lerp body position
                    transform.Position += (physics.Body.Position - transform.Position) * accumulator / StepTime;

                    // TODO: Negative rotation?
                    transform.Rotation += (-physics.Body.Rotation - transform.Rotation) * accumulator / StepTime;
                }
            }
        }
    }
}
