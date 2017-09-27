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
            /*for (int i = 0; i < 1280; i++)
            {
                Entity entity = game.world.Create();

                Transform transform = new Transform()
                {
                    position = new Vector2((i % 40) * 32 + 16, (i / 40) * 32 + 16),
                    rotation = 20 * i / 32
                };

                entity.AddComponent(transform);
                entity.AddComponent(new SpriteRenderer(ed));
            }*/

            Texture face = Textures.Load("Textures/Face.png");
            Scene game = new Scene();

            Entity entity = game.world.Create();
            entity.AddComponent(new Transform());
            entity.AddComponent(new SpriteRenderer(face));
            entity.AddComponent(new Rigidbody());

            //Map map = new Map("Maps/Test.tmx");

            game.world.AddSystem(new FPSCounterSystem());

            Console.WriteLine("Setup complete");

            Load(game);
        }
    }
}
