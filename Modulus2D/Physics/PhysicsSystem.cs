using FarseerPhysics.Dynamics;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Network;
using System;
using Lidgren.Network;
using Modulus2D.Math;

namespace Modulus2D.Physics
{
    public delegate void RaycastCallback(bool hit, Vector2 point, Vector2 normal, float fraction);

    public class PhysicsSystem : EntitySystem
    {
        private EntityFilter filter;
        private EntityFilter networkFilter;
        private ComponentStorage<TransformComponent> transformComponents;
        private ComponentStorage<PhysicsComponent> rigidbodyComponents;
        private ComponentStorage<NetComponent> netComponents;

        private World physicsWorld;
        private float stepTime = 1 / 60f;
        private float accumulator = 0f;
        
        public float StepTime { get => stepTime; set => stepTime = value; }
        public World PhysicsWorld { get => physicsWorld; set => physicsWorld = value; }

        public PhysicsSystem()
        {
            PhysicsWorld = new World(new Vector2(0f, -9.8f));
        }

        public override void OnAdded()
        {
            transformComponents = World.GetStorage<TransformComponent>();
            rigidbodyComponents = World.GetStorage<PhysicsComponent>();
            netComponents = World.GetStorage<NetComponent>();

            filter = new EntityFilter(transformComponents, rigidbodyComponents);
            networkFilter = new EntityFilter(rigidbodyComponents, netComponents);

            World.AddCreatedListener<PhysicsComponent>(entity =>
            {
                PhysicsComponent rigidbody = rigidbodyComponents.Get(entity);
                rigidbody.Init(PhysicsWorld);
            });
            World.AddRemovedListener<PhysicsComponent>(entity =>
            {
                PhysicsComponent rigidbody = rigidbodyComponents.Get(entity);
                PhysicsWorld.RemoveBody(rigidbody.Body);
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
                Body body = rigidbodyComponents.Get(id).Body;

                // Lerp body position
                transform.Position += (new Vector2(body.Position.X, body.Position.Y) - transform.Position) * accumulator / StepTime;

                // TODO: Negative rotation?
                transform.Rotation += (-body.Rotation - transform.Rotation) * accumulator / StepTime;
            }
        }
    }
}