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
    class GameState : State
    {
        private EntityWorld entityWorld;

        public override void Start()
        {
            entityWorld = new EntityWorld();

            // Add physics system
            PhysicsSystem physicsSystem = new PhysicsSystem();
            entityWorld.AddSystem(physicsSystem);

            // Add sprite system
            SpriteBatch batch = new SpriteBatch(Target);
            SpriteSystem spriteSystem = new SpriteSystem(batch)
            {
                Priority = -1 // Render sprites last
            };
            entityWorld.AddSystem(spriteSystem);

            // Add map system
            MapSystem maps = new MapSystem(batch)
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
            

            // Load textures
            Texture face = new Texture("Resources/Textures/Face.png");

            // Create camera
            OrthoCamera camera = new OrthoCamera(new Viewport(Target.Size.X, Target.Size.Y, 10f))
            {
                Position = new Vector2(0f, 0f)
            };
            batch.Camera = camera;

            // Add player system
            PlayerSystem playerSystem = new PlayerSystem();
            entityWorld.AddSystem(playerSystem);
            
            // Create player
            Entity player = entityWorld.Create();
            player.AddComponent(new TransformComponent());
            player.AddComponent(new SpriteComponent(face));
            player.AddComponent(new PhysicsComponent());
            player.AddComponent(new PlayerComponent());
            player.AddComponent(new CircleCollider(0.5f));
            player.GetComponent<PhysicsComponent>().Body.Position = new Vector2(0f, 0f);

            // Add camera system
            CameraSystem cameraSystem = new CameraSystem(camera, player.GetComponent<TransformComponent>());
            entityWorld.AddSystem(cameraSystem);

            // Add FPS counter
            entityWorld.AddSystem(new FPSCounterSystem());
        }

        public override void Update(float deltaTime)
        {            
            entityWorld.Update(deltaTime);
        }
    }
}
