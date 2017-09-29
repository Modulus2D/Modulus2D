using Prota2D.Core;
using Prota2D.Entities;
using Prota2D.Graphics;
using Prota2D.Math;
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

            Texture face = game.Textures.Load("Resources/Textures/Test.png");

            Entity entity = scene.world.Create();
            entity.AddComponent(new Transform());
            entity.AddComponent(new SpriteRenderer(face));
            entity.AddComponent(new Rigidbody());
            entity.AddComponent(new CircleCollider(1f));

            Entity floor = scene.world.Create();
            floor.AddComponent(new Transform());
            floor.AddComponent(new SpriteRenderer(face));
            floor.AddComponent(new Rigidbody());
            floor.AddComponent(new BoxCollider(0.5f, 0.5f));
            floor.GetComponent<Rigidbody>().IsStatic = true;
            floor.GetComponent<Rigidbody>().Position = new Vector2(0f, 3f);

            //Map map = new Map("Maps/Test.tmx");

            scene.world.AddSystem(new FPSCounterSystem());

            game.Start();
        }
    }
}
