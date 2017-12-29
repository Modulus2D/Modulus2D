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

        public PhysicsSystem()
        {
            physicsWorld = new World(Vector2.Convert(new Vector2(0f, 9.8f)));
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
                rigidbody.Init(physicsWorld);
            });
            World.AddRemovedListener<PhysicsComponent>(entity =>
            {
                PhysicsComponent rigidbody = rigidbodyComponents.Get(entity);
                physicsWorld.RemoveBody(rigidbody.Body);
            });
        }

        public void Raycast(Vector2 point1, Vector2 point2, RaycastCallback callback)
        {
            physicsWorld.RayCast((fixture, point, normal, fraction) =>
            {
                callback(fixture != null, Vector2.Convert(point), Vector2.Convert(normal), fraction);
                return fraction;
            }, Vector2.Convert(point1), Vector2.Convert(point2));
        }

        public override void Update(float deltaTime)
        {
            // Update world
            accumulator += deltaTime;

            while (accumulator >= StepTime)
            {
                physicsWorld.Step(StepTime);
                accumulator -= StepTime;
            }

            // Update transforms
            foreach (int id in World.Iterate(filter))
            {
                TransformComponent transform = transformComponents.Get(id);
                PhysicsComponent rigidbody = rigidbodyComponents.Get(id);

                // Lerp body position
                transform.Position += (rigidbody.Position - transform.Position) * accumulator / StepTime;

                // TODO: Negative rotation?
                transform.Rotation += (-rigidbody.Rotation - transform.Rotation) * accumulator / StepTime;
            }
        }
    }
}