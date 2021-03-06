﻿using FarseerPhysics.Dynamics;
using Modulus2D.Entities;
using Modulus2D.Math;
using Modulus2D.Network;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        
        private float maxPositionDistance = 0.2f;
        private float predictLerp = 0.2f;

        private float delta = 0f;

        // Stopwatch for interpolation
        private Stopwatch stopwatch;

        public ClientPhysicsSystem(ClientSystem clientSystem)
        {
            stopwatch = new Stopwatch();

            clientSystem.UpdateReceived += (delta) =>
            {
                Console.WriteLine("UPDATE");

                this.delta = delta;

                stopwatch.Restart();
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
                Body body = physics.Body;

                switch(physics.Mode)
                {
                    case PhysicsComponent.NetMode.Raw:
                        body.Position = physics.CorrectPosition;

                        break;
                    case PhysicsComponent.NetMode.Interpolate:
                        if (delta != 0f && physics.LastPosition != null && physics.CorrectPosition != null)
                        {
                            body.Position = physics.LastPosition + (physics.CorrectPosition - physics.LastPosition) * ((float)stopwatch.Elapsed.TotalSeconds / delta);
                            body.Rotation = physics.CorrectRotation + (physics.CorrectRotation - physics.LastRotation) * (float)stopwatch.Elapsed.TotalSeconds / delta;
                        }

                        break;
                    case PhysicsComponent.NetMode.Predict:
                        float distance = Vector2.Distance(body.Position, physics.LastPosition);
                        if (distance > MaxPositionDistance && physics.LastPosition != null && physics.CorrectPosition != null)
                        {
                            body.Position += (physics.LastPosition - body.Position) * PredictLerp * deltaTime * distance;
                        }

                        // physics.Body.Rotation += (physics.correctRotation - physics.Body.Rotation) * PredictLerp * deltaTime;
                        // physics.Body.LinearVelocity += (physics.correctVelocity - physics.Body.LinearVelocity) * PredictLerp * deltaTime;

                        break;
                    case PhysicsComponent.NetMode.Client:
                        break;
                }
            }
        }
    }
}