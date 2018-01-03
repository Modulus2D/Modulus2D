using Example;
using FarseerPhysics.Dynamics;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Input;
using Modulus2D.Map;
using Modulus2D.Math;
using Modulus2D.Network;
using Modulus2D.Physics;
using Modulus2D.Utility;
using SFML.Window;
using System;

namespace ExampleGame
{
    class GameState : State
    {
        private EntityWorld world;
        private SpriteBatch batch;
        private ClientSystem clientSystem;

        public override void Start()
        {
            world = new EntityWorld();
            
            // Add physics system
            PhysicsSystem physicsSystem = new PhysicsSystem()
            {
                Priority = -1
            };
            world.AddSystem(physicsSystem);

            // Camera
            OrthoCamera camera = new OrthoCamera(Graphics.Width, Graphics.Height);

            // Sprite bacth
            batch = new SpriteBatch(Graphics, camera);

            // Add sprite system
            SpriteSystem spriteSystem = new SpriteSystem(batch)
            {
                Priority = -2
            };
            world.AddSystem(spriteSystem);

            // Add map system
            MapSystem mapSystem = new MapSystem(batch);
            world.AddSystem(mapSystem);
            
            // Load map
            Entity map = world.Create();
            map.AddComponent(new TransformComponent());
            map.AddComponent(new PhysicsComponent());
            map.AddComponent(new MapComponent("Maps/Test.tmx"));

            // Create debug system
            OneShotInput reload = new OneShotInput();
            Input.Add(reload);

            reload.AddKey(Keyboard.Key.R, 1f);
            world.AddSystem(new DebugSystem(mapSystem, reload));

            // Create camera
            camera.Size = 8f;

            // Add player system
            PlayerSystem playerSystem = new PlayerSystem(physicsSystem);
            world.AddSystem(playerSystem);

            // Add player input system
            ContinuousInput moveX = new ContinuousInput();
            Input.Add(moveX);

            moveX.AddKey(Keyboard.Key.D, 1f);
            moveX.AddKey(Keyboard.Key.A, -1f);
            moveX.AddKey(Keyboard.Key.Right, 1f);
            moveX.AddKey(Keyboard.Key.Left, -1f);

            OneShotInput jump = new OneShotInput();
            Input.Add(jump);

            jump.AddKey(Keyboard.Key.W, 1f);
            jump.AddKey(Keyboard.Key.Up, 1f);
            jump.AddGamepadButton(Xbox.A, 1f);

            world.AddSystem(new PlayerInputSystem(moveX, jump));

            // Add camera system
            CameraSystem cameraSystem = new CameraSystem(camera);
            world.AddSystem(cameraSystem);

            // Add client system
            clientSystem = new ClientSystem("127.0.0.1", 14357);
            world.AddSystem(clientSystem);
            
            Entity entity = world.Create();
            entity.AddComponent(new TransformComponent());

            PlayerComponent player = new PlayerComponent();
            entity.AddComponent(player);

            // Add wheel graphics
            SpriteComponent sprites = new SpriteComponent(new Texture("Textures/Wheel.png"));
            entity.AddComponent(sprites);

            Body body = entity.GetComponent<PhysicsComponent>().Body;
            body.Position = new Vector2(0f, 10f);

            cameraSystem.targets.Add(entity.GetComponent<TransformComponent>());
            entity.AddComponent(new PlayerInputComponent());

            /*clientSystem.RegisterEntity("player", (entity, args) =>
            {
                entity.AddComponent(new TransformComponent());

                PlayerComponent player = new PlayerComponent();
                entity.AddComponent(player);
                
                // Add graphics
                SpriteRendererComponent sprites = new SpriteRendererComponent();
                sprites.AddSprite(new Texture("Textures/Wheel.png"));
                sprites.AddSprite(new Texture("Textures/Face.png"));
                entity.AddComponent(sprites);
                
                PhysicsComponent physics = entity.GetComponent<PhysicsComponent>();
                physics.Mode = PhysicsComponent.NetMode.Interpolate;

                NetComponent network = entity.GetComponent<NetComponent>();

                network.AddReceiver(physics);
            });

            clientSystem.RegisterEvent("control", (args) =>
            {
                Console.WriteLine("Control");

                Entity entity = clientSystem.GetByNetId((uint)args[0]);

                // Follow with camera
                cameraSystem.targets.Add(entity.GetComponent<TransformComponent>());
                entity.AddComponent(new PlayerInputComponent());

                entity.GetComponent<PhysicsComponent>().Mode = PhysicsComponent.NetMode.Predict;
                entity.GetComponent<NetComponent>().AddTransmitter(entity.GetComponent<PlayerComponent>());
            });*/

            world.AddSystem(new ClientPhysicsSystem(clientSystem));
            
            // Add FPS counter
            world.AddSystem(new FPSCounterSystem());
        }

        public override void Update(float deltaTime)
        {         
            // Update world
            world.Update(deltaTime);

            // Render world
            world.Render(deltaTime);

            // Render last batch
            batch.Flush();
        }

        public override void Close()
        {
            clientSystem.Disconnect();
        }
    }
}