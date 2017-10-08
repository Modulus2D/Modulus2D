using Microsoft.Xna.Framework;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Map;
using Modulus2D.Physics;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class ServerState : State
    {
        private EntityWorld entityWorld;

        public override void Start()
        {
            entityWorld = new EntityWorld();

            // Add physics system
            PhysicsSystem physicsSystem = new PhysicsSystem();
            entityWorld.AddSystem(physicsSystem);

            // Add map system
            MapSystem maps = new MapSystem()
            {
                Priority = -1 // Render map last
            };
            entityWorld.AddSystem(maps);

            // Load map
            Entity map = entityWorld.Create();
            map.AddComponent(new TransformComponent());
            map.AddComponent(new PhysicsComponent());
            map.GetComponent<PhysicsComponent>().Body.IsStatic = true;
            map.AddComponent(new MapComponent("Resources/Maps/Test.tmx"));

            // Create debug system
            entityWorld.AddSystem(new DebugSystem(maps));

            // Add player system
            PlayerSystem playerSystem = new PlayerSystem();
            entityWorld.AddSystem(playerSystem);
            
            // Create player
            Entity player = entityWorld.Create();
            player.AddComponent(new TransformComponent());
            player.AddComponent(new PhysicsComponent());
            player.AddComponent(new PlayerComponent());
            player.AddComponent(new CircleCollider(0.5f));
            player.GetComponent<PhysicsComponent>().Body.Position = new Vector2(0f, 0f);

            // Add FPS counter
            entityWorld.AddSystem(new FPSCounterSystem());
        }

        public override void Update(float deltaTime)
        {            
            entityWorld.Update(deltaTime);
        }
    }
}
