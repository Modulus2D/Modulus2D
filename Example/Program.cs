using Microsoft.Xna.Framework;
using Prota2D.Core;
using Prota2D.Entities;
using Prota2D.Graphics;
using Prota2D.Physics;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(1280, 768, "Example");

            Scene scene = new Scene();
            game.Load(scene);

            SFML.Graphics.Texture face = new SFML.Graphics.Texture("Resources/Textures/Test.png");

            Entity entity = scene.world.Create();
            entity.AddComponent(new Transform());
            entity.AddComponent(new SpriteRenderer(face));
            entity.AddComponent(new Rigidbody());
            entity.AddComponent(new CircleCollider(0.5f));

            Entity floor = scene.world.Create();
            floor.AddComponent(new Transform());
            floor.AddComponent(new SpriteRenderer(face));
            floor.AddComponent(new Rigidbody());
            floor.AddComponent(new BoxCollider(1f, 1f));
            floor.GetComponent<Rigidbody>().Body.IsStatic = true;
            floor.GetComponent<Rigidbody>().Body.Position = new Vector2(0f, 3f);
                        
            OrthoCamera camera = new OrthoCamera(new Viewport(game.Window.Width, game.Window.Height, 10f))
            {
                Position = new Vector2(0f, 0f)
            };

            game.Window.SetCamera(camera);

            //Map map = new Map("Maps/Test.tmx");

            scene.world.AddSystem(new FPSCounterSystem());

            game.Start();
        }
    }
}
