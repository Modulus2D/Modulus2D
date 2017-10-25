using Microsoft.Xna.Framework;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Map;
using Modulus2D.Network;
using Modulus2D.Physics;
using Modulus2D.UI;
using Modulus2D.Player;
using SFML.Graphics;
using SFML.System;
using Modulus2D.Player.Platformer;
using Example;
using Modulus2D.Input;
using SFML.Window;
using System;
using FarseerPhysics.Dynamics;

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

            Entity text = world.Add();

            // Load map
            Entity map = world.Add();
            map.AddComponent(new TransformComponent());
            map.AddComponent(new PhysicsComponent());
            map.GetComponent<PhysicsComponent>().Body.IsStatic = true;
            map.AddComponent(new MapComponent("Maps/Test.tmx"));

            // Create debug system
            OneShotInput reload = new OneShotInput();
            Input.Add(reload);

            reload.AddKey(Keyboard.Key.R, 1f);

            world.AddSystem(new DebugSystem(maps, reload));

            // Create camera
            OrthoCamera camera = new OrthoCamera(new Viewport(Graphics.Size.X, Graphics.Size.Y, 10f))
            {
                Position = new Vector2(0f, 0f)
            };
            batch.Camera = camera;

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

            // Add FPS counter
            world.AddSystem(new FPSCounterSystem());
            
            // Add network system
            NetSystem networkSystem = new NetSystem();
            world.AddSystem(networkSystem);
            
            // Add client system
            ClientSystem clientSystem = new ClientSystem(networkSystem, "45.55.179.215", 14357);
            
            clientSystem.RegisterEvent("CreatePlayer", (args) =>
            {
                Entity entity = world.Add();

                entity.AddComponent(new TransformComponent());

                PlayerComponent player = new PlayerComponent();
                entity.AddComponent(player);

                NetComponent network = new NetComponent((uint)args[0]);
                entity.AddComponent(network);
                
                // Add graphics
                SpriteRendererComponent sprites = new SpriteRendererComponent();
                sprites.AddSprite(new Texture("Textures/Wheel.png"));
                sprites.AddSprite(new Texture("Textures/Face.png"));
                entity.AddComponent(sprites);

                // Interpolate position
                entity.GetComponent<PhysicsComponent>().Mode = PhysicsComponent.NetMode.Interpolate;

                // Receive physics information
                network.AddReceiver(entity.GetComponent<PhysicsComponent>());
            });
            
            clientSystem.RegisterEvent("ControlPlayer", (args) =>
            {
                Entity entity = networkSystem.GetByNetId((uint)args[0]);

                // Add input component
                entity.AddComponent(new PlayerInputComponent());

                NetComponent network = entity.GetComponent<NetComponent>();

                // Transmit player information
                PlayerComponent player = entity.GetComponent<PlayerComponent>();
                network.AddTransmitter(player);

                // Predict position
                entity.GetComponent<PhysicsComponent>().Mode = PhysicsComponent.NetMode.Predict;

                // Follow with camera
                cameraSystem.targets.Add(entity.GetComponent<TransformComponent>());
            });

            world.AddSystem(clientSystem);

            world.AddSystem(new ClientPhysicsSystem(networkSystem));
        }

        public override void Update(float deltaTime)
        {            
            world.Update(deltaTime);
        }
    }
}