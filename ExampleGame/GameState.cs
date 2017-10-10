﻿using Example;
using Microsoft.Xna.Framework;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Map;
using Modulus2D.Network;
using Modulus2D.Physics;
using Modulus2D.UI;
using SFML.Graphics;
using SFML.System;

namespace ExampleGame
{
    class GameState : State
    {
        private EntityWorld world;

        public override void Start()
        {
            world = new EntityWorld();

            // Add physics system
            PhysicsSystem physicsSystem = new PhysicsSystem();
            world.AddSystem(physicsSystem);
                        
            // Add sprite system
            SpriteBatch batch = new SpriteBatch(Graphics);
            SpriteSystem spriteSystem = new SpriteSystem(batch)
            {
                Priority = -2 // Render sprites last
            };
            world.AddSystem(spriteSystem);

            // Add map system
            MapSystem maps = new MapSystem(batch)
            {
                Priority = -1 // Render map last
            };
            world.AddSystem(maps);

            // Add UI system
            world.AddSystem(new UISystem(Graphics));

            Entity text = world.Create();

            // Load map
            Entity map = world.Create();
            map.AddComponent(new TransformComponent());
            map.AddComponent(new PhysicsComponent());
            map.GetComponent<PhysicsComponent>().Body.IsStatic = true;
            map.AddComponent(new MapComponent("Resources/Maps/Test.tmx"));

            // Create debug system
            // world.AddSystem(new DebugSystem(maps));

            // Load textures
            Texture face = new Texture("Resources/Textures/Face.png");

            // Create camera
            OrthoCamera camera = new OrthoCamera(new Viewport(Graphics.Size.X, Graphics.Size.Y, 10f))
            {
                Position = new Vector2(0f, 0f)
            };
            batch.Camera = camera;

            // Add player system
            PlayerSystem playerSystem = new PlayerSystem();
            world.AddSystem(playerSystem);
            
            // Create player
            Entity player = world.Create();
            player.AddComponent(new TransformComponent());
            player.AddComponent(new SpriteComponent(face));
            player.AddComponent(new PlayerComponent());
            PhysicsComponent playerPhysics = new PhysicsComponent();
            player.AddComponent(playerPhysics);
            playerPhysics.CreateCircle(0.5f, 1f);

            // Add camera system
            CameraSystem cameraSystem = new CameraSystem(camera, player.GetComponent<TransformComponent>());
            world.AddSystem(cameraSystem);

            // Add FPS counter
            world.AddSystem(new FPSCounterSystem());

            // Add network system
            NetworkSystem networkSystem = new NetworkSystem();
            world.AddSystem(networkSystem);
            
            // Add client system
            world.AddSystem(new ClientSystem(networkSystem, "127.0.0.1", 14356));
        }

        public override void Update(float deltaTime)
        {            
            world.Update(deltaTime);
        }
    }
}