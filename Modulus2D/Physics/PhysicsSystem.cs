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
        private float stepTime = 1 / 60f;
        private float accumulator = 0f;

        public PhysicsSystem()
        {
            PhysicsWorld = new World(new Vector2(0f, 9.8f));

            filter.Add<TransformComponent>();
            filter.Add<PhysicsComponent>();
        }

        public override void AddedToWorld()
        {
            World.AddCreationListener<PhysicsComponent>(Created);
            World.AddDestructionListener<PhysicsComponent>(Destroyed);

            // To be removed
            World.AddCreationListener<CircleCollider>(CircleColliderAdded);
            World.AddCreationListener<BoxCollider>(BoxColliderAdded);
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

        // TODO: Remove specific colliders
        public void CircleColliderAdded(Entity entity)
        {
            PhysicsComponent physics = entity.GetComponent<PhysicsComponent>();

            if(physics == null)
            {
                //logger.Error("Attempted to add collider before adding rigidbody");
                return;
            }

            CircleCollider collider = entity.GetComponent<CircleCollider>();
            collider.Init(physics.Body);
        }

        public void BoxColliderAdded(Entity entity)
        {
            PhysicsComponent physics = entity.GetComponent<PhysicsComponent>();

            if (physics == null)
            {
                //logger.Error("Attempted to add collider before adding rigidbody");
                return;
            }

            BoxCollider collider = entity.GetComponent<BoxCollider>();
            collider.Init(physics.Body);
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
                    transform.Position = physics.Body.Position;
                    transform.Rotation = -physics.Body.Rotation;
                }
            }
        }
    }
}
