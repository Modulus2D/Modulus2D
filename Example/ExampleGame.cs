using System;
using Microsoft.Xna.Framework;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Physics;
using Prota2D.Core;
using SFML.Graphics;

namespace Example
{
    class ExampleGame : IGame
    {
        OrthoCamera camera;
        Map map;

        public Config Config()
        {
            return new Config("Example", 1336, 768);
        }

        public void Start(App game)
        {
            Scene scene = new Scene();
            game.Load(scene);

            Texture face = new Texture("Resources/Textures/Face.png");
            Texture test = new Texture("Resources/Textures/Test.png");

            Entity entity = scene.world.Create();
            entity.AddComponent(new TransformComponent());
            entity.AddComponent(new SpriteComponent(face));
            entity.AddComponent(new PhysicsComponent());
            entity.AddComponent(new CircleCollider(0.5f));

            Entity floor = scene.world.Create();
            floor.AddComponent(new TransformComponent());
            floor.AddComponent(new SpriteComponent(test));
            floor.AddComponent(new PhysicsComponent());
            floor.AddComponent(new BoxCollider(1f, 1f));
            floor.GetComponent<PhysicsComponent>().Body.IsStatic = true;
            floor.GetComponent<PhysicsComponent>().Body.Position = new Vector2(0f, 3f);

            camera = new OrthoCamera(new Viewport(1336, 768, 10f))
            {
                Position = new Vector2(0f, 0f)
            };

            map = new Map();
            map.Load("Resources/Maps/Test.tmx");

            scene.world.AddSystem(new FPSCounterSystem());
            scene.world.AddSystem(new DebugSystem(map));
            scene.world.AddSystem(new PlayerSystem(camera, entity));
        }

        public void Update(float deltaTime, RenderTarget target, Scene scene)
        {
            camera.Update();
            target.SetView(camera.View);
            map.Draw(target);
        }
    }
}
