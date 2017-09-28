using FarseerPhysics.Dynamics;
using NLog;
using Prota2D.Core;
using Prota2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Physics
{
    class PhysicsSystem : EntitySystem
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private EntityFilter filter = new EntityFilter();
        private PhysicsWorld world;

        public PhysicsSystem(PhysicsWorld physicsWorld)
        {
            world = physicsWorld;

            filter.Add<Transform>();
            filter.Add<Rigidbody>();
        }

        public override void Init(EntityWorld world)
        {
            world.AddListener<Rigidbody>(Created);
            world.AddListener<CircleCollider>(BoxColliderAdded);
        }

        public void Created(Entity entity)
        {
            Rigidbody rigidbody = entity.GetComponent<Rigidbody>();
            rigidbody.Init(world.world);
        }

        public void BoxColliderAdded(Entity entity)
        {
            Rigidbody rigidbody = entity.GetComponent<Rigidbody>();
            
            if(rigidbody == null)
            {
                logger.Error("Attempted to add collider before adding rigidbody");
                return;
            }

            CircleCollider collider = entity.GetComponent<CircleCollider>();
            collider.Init(rigidbody.body);
        }

        public override void Update(EntityWorld world, float deltaTime)
        {
            foreach(Components components in world.Iterate(filter))
            {
                Transform transform = components.Next<Transform>();
                Rigidbody rigidbody = components.Next<Rigidbody>();

                if(rigidbody.body != null)
                {
                    transform.Position.X = rigidbody.body.Position.X;
                    transform.Position.Y = rigidbody.body.Position.Y;
                    transform.Rotation = rigidbody.body.Rotation;
                }
            }
        }
    }
}
