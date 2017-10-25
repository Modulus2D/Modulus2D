using Microsoft.Xna.Framework;
using Modulus2D.Entities;
using Modulus2D.Network;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Physics
{
    public class ClientPhysicsSystem : EntitySystem
    {
        private EntityFilter filter;
        private ComponentStorage<NetComponent> netComponents;
        private ComponentStorage<PhysicsComponent> physicsComponents;

        private NetSystem networkSystem;

        private float maxPositionDistance = 0f;
        private float predictLerp = 0.03f;

        // Clock for interpolation
        private Clock clock;

        public ClientPhysicsSystem(NetSystem networkSystem)
        {
            this.networkSystem = networkSystem;

            clock = new Clock();

            this.networkSystem.ReceiveUpdate += () =>
            {
                clock.Restart();
            };
        }

        /// <summary>
        /// Amount of linear interpolation to apply when correcting predictions
        /// </summary>
        public float PredictLerp { get => predictLerp; set => predictLerp = value; }

        /// <summary>
        /// Distance at which position is corrected
        /// </summary>
        public float MaxPositionDistance { get => maxPositionDistance; set => maxPositionDistance = value; }

        public override void OnAdded()
        {
            netComponents = World.GetStorage<NetComponent>();
            physicsComponents = World.GetStorage<PhysicsComponent>();

            filter = new EntityFilter(netComponents, physicsComponents);
        }

        public override void Update(float deltaTime)
        {
            // Update transforms
            foreach (int id in World.Iterate(filter))
            {
                NetComponent network = netComponents.Get(id);
                PhysicsComponent physics = physicsComponents.Get(id);

                switch(physics.Mode)
                {
                    case PhysicsComponent.NetMode.Raw:
                        physics.Body.Position = physics.correctPosition;

                        break;
                    case PhysicsComponent.NetMode.Interpolate:
                        float delta = networkSystem.LastDelta;
                        if (delta != 0f && physics.lastPosition != null && physics.correctPosition != null)
                        {
                            physics.Body.Position = Vector2.Lerp(physics.lastPosition, physics.correctPosition, clock.ElapsedTime.AsSeconds() / delta);
                            physics.Body.Rotation = physics.correctRotation + (physics.correctRotation - physics.lastRotation) * clock.ElapsedTime.AsSeconds() / delta;
                        }

                        break;
                    case PhysicsComponent.NetMode.Predict:
                        float distance = Vector2.Distance(physics.Body.Position, physics.correctPosition);
                        if (distance > MaxPositionDistance && physics.lastPosition != null && physics.correctPosition != null)
                        {
                            physics.Body.Position += (physics.correctPosition - physics.Body.Position) * PredictLerp * distance;
                            // physics.Body.Rotation += (physics.correctRotation - physics.Body.Rotation) * deltaTime * PredictLerp;
                        }
                        physics.Body.Rotation = 0f;

                        break;
                    case PhysicsComponent.NetMode.Client:
                        break;
                }
            }
        }
    }
}
