using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D;
using Prota2D.Entities;
using Prota2D.Graphics;
using Prota2D.Core;
using Prota2D.Math;
using Prota2D.Physics;

namespace Example
{
    class ExampleGame : Game
    {
        public override void Init()
        {  
            Scene game = new Scene();
            Load(game);

            Texture face = Textures.Load("Resources/Textures/Face.png");

            Entity entity = game.world.Create();
            entity.AddComponent(new Transform());
            entity.AddComponent(new SpriteRenderer(face));
            entity.AddComponent(new Rigidbody());
            entity.AddComponent(new CircleCollider(1f));

            Entity floor = game.world.Create();
            floor.AddComponent(new Transform());
            floor.AddComponent(new Rigidbody());
            floor.AddComponent(new CircleCollider(1f));
            floor.GetComponent<Rigidbody>().IsStatic = true;

            //Map map = new Map("Maps/Test.tmx");

            game.world.AddSystem(new FPSCounterSystem());

            Console.WriteLine("Setup complete");
        }
    }
}
