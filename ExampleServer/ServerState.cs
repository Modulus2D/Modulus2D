using Microsoft.Xna.Framework;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Map;
using Modulus2D.Network;
using Modulus2D.Physics;
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
            Entity map = world.Create();
            map.AddComponent(new TransformComponent());
            map.AddComponent(new PhysicsComponent());
            map.GetComponent<PhysicsComponent>().Body.IsStatic = true;
            map.AddComponent(new MapComponent("Resources/Maps/Test.tmx"));

            // Create debug system
            //entityWorld.AddSystem(new DebugSystem(maps));
            
            // Add FPS counter
            world.AddSystem(new FPSCounterSystem());

            // Add network system
            NetworkSystem networkSystem = new NetworkSystem();
            world.AddSystem(networkSystem);

            // Create server system
            world.AddSystem(new ServerSystem(networkSystem, 14356));
        }

        public override void Update(float deltaTime)
        {
            world.Update(deltaTime);
        }
    }
}
