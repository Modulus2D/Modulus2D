using Example;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Map;
using Modulus2D.Network;
using Modulus2D.Physics;
using Modulus2D.Player.Platformer;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleServer
{
    class ServerState : State
    {
        private EntityWorld world;

        public override void Start()
        {
            world = new EntityWorld();

            // Add physics system
            PhysicsSystem physicsSystem = new PhysicsSystem();
            world.AddSystem(physicsSystem);

            // Add map system
            MapSystem maps = new MapSystem()
            {
                Priority = -1 // Render map last
            };
            world.AddSystem(maps);

            // Load map
            Entity map = world.Add();
            map.AddComponent(new TransformComponent());
            map.AddComponent(new PhysicsComponent());
            map.GetComponent<PhysicsComponent>().Body.IsStatic = true;
            map.AddComponent(new MapComponent("Resources/Maps/Test.tmx"));

            // Add player system
            PlayerSystem playerSystem = new PlayerSystem(physicsSystem);
            world.AddSystem(playerSystem);

            // Create debug system
            //entityWorld.AddSystem(new DebugSystem(maps));

            // Add FPS counter
            world.AddSystem(new FPSCounterSystem());

            // Add network system
            NetworkSystem networkSystem = new NetworkSystem();
            world.AddSystem(networkSystem);

            PlayerBuilder builder = new PlayerBuilder();

            // Create server system
            ServerSystem serverSystem = new ServerSystem(networkSystem, 14357);

            // serverSystem.RegisterBuilder(builder);

            world.AddSystem(serverSystem);

            // Create player on connection
            serverSystem.Connect += () =>
            {
                Entity entity = world.Add();

                entity.AddComponent(new TransformComponent());

                PhysicsComponent physics = new PhysicsComponent();
                entity.AddComponent(physics);

                Fixture fixture = physics.CreateCircle(0.5f, 0.59f);
                fixture.Friction = 10f;

                PlayerComponent player = new PlayerComponent();
                entity.AddComponent(player);

                NetworkComponent network = new NetworkComponent();
                entity.AddComponent(network);

                // Transmit physics information
                network.AddTransmitter(entity.GetComponent<PhysicsComponent>());

                // Receive input information
                network.AddReceiver(entity.GetComponent<PlayerComponent>());
            };
        }

        public override void Update(float deltaTime)
        {
            world.Update(deltaTime);
        }
    }
}
