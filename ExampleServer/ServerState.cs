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
            Entity map = world.Create();
            map.AddComponent(new TransformComponent());
            map.AddComponent(new Rigidbody());
            map.GetComponent<Rigidbody>().Body.IsStatic = true;
            map.AddComponent(new MapComponent("Maps/Test.tmx"));

            // Add player system
            PlayerSystem playerSystem = new PlayerSystem(physicsSystem);
            world.AddSystem(playerSystem);

            // Create debug system
            //entityWorld.AddSystem(new DebugSystem(maps));

            // Add FPS counter
            world.AddSystem(new FPSCounterSystem());

            // Create server system
            ServerSystem serverSystem = new ServerSystem(14357);

            world.AddSystem(serverSystem);

            Dictionary<NetPlayer, Entity> players = new Dictionary<NetPlayer, Entity>();

            // Create player on connection
            serverSystem.Connected += (player) =>
            {
                Entity entity = serverSystem.Create("player");
                players[player] = entity;

                // Tell player to send input
                serverSystem.SendEvent(player, "control", entity.GetComponent<NetComponent>().Id);

                Console.WriteLine("Created player");
            };

            serverSystem.Disconnected += (player) =>
            {
                // Destroy player
                players[player].Destroy();

                Console.WriteLine("Destroyed player");
            };

            serverSystem.RegisterEntity("player", (entity, args) =>
            {
                entity.AddComponent(new TransformComponent());

                PlayerComponent player = new PlayerComponent();
                entity.AddComponent(player);

                NetComponent network = entity.GetComponent<NetComponent>();

                network.AddTransmitter(entity.GetComponent<Rigidbody>());
                network.AddReceiver(entity.GetComponent<PlayerComponent>());
            });

            serverSystem.Disconnected += (netPlayer) =>
            {
            };
        }

        public override void Update(float deltaTime)
        {
            world.Update(deltaTime);
        }
    }
}
