using FarseerPhysics.Dynamics;
using NLog;
using Modulus2D.Core;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Physics
{
    class PhysicsSystem : EntitySystem
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private EntityFilter filter = new EntityFilter();
        private PhysicsWorld physicsWorld;

        public PhysicsSystem(PhysicsWorld physicsWorld)
        {
            this.physicsWorld = physicsWorld;

            filter.Add<TransformComponent>();
            filter.Add<PhysicsComponent>();
        }

        public override void Start()
        {
            World.AddListener<PhysicsComponent>(Created);
            World.AddListener<CircleCollider>(CircleColliderAdded);
            World.AddListener<BoxCollider>(BoxColliderAdded);
        }

        public void Created(Entity entity)
        {
            PhysicsComponent rigidbody = entity.GetComponent<PhysicsComponent>();
            rigidbody.Init(physicsWorld.world);
        }

        public void CircleColliderAdded(Entity entity)
        {
            PhysicsComponent rigidbody = entity.GetComponent<PhysicsComponent>();

            if(rigidbody == null)
            {
                logger.Error("Attempted to add collider before adding rigidbody");
                return;
            }

            CircleCollider collider = entity.GetComponent<CircleCollider>();
            collider.Init(rigidbody.Body);
        }

        public void BoxColliderAdded(Entity entity)
        {
            PhysicsComponent rigidbody = entity.GetComponent<PhysicsComponent>();

            if (rigidbody == null)
            {
                logger.Error("Attempted to add collider before adding rigidbody");
                return;
            }

            BoxCollider collider = entity.GetComponent<BoxCollider>();
            collider.Init(rigidbody.Body);
        }

        public override void Update(float deltaTime)
        {
            foreach(Components components in World.Iterate(filter))
            {
                TransformComponent transform = components.Next<TransformComponent>();
                PhysicsComponent rigidbody = components.Next<PhysicsComponent>();
                
                if (rigidbody.Body != null)
                {
                    transform.Position = rigidbody.Body.Position;
                    transform.Rotation = -rigidbody.Body.Rotation;
                }
            }
        }
    }
}
