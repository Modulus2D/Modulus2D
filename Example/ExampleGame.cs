using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D;
using Prota2D.Entities;
using Prota2D.Graphics;

namespace Example
{
    class ExampleGame : Game
    {
        public override void Init()
        {
            Console.WriteLine("Init");

            Texture ed = Textures.Load("Textures/Dota.png");
            /*Texture disc = Textures.Load("Textures/Dota.png");
            Texture harold = Textures.Load("Textures/Dota.png");
            Texture gerald = Textures.Load("Textures/Dota.png");
            Texture pillar = Textures.Load("Textures/Dota.png");*/

            
            
            Texture disc = Textures.Load("Textures/Face.png");
            Texture harold = Textures.Load("Textures/Harold2.png");
            Texture gerald = Textures.Load("Textures/Gerald2.png");
            Texture pillar = Textures.Load("Textures/Pillar.png");

    

            for (int i = 0; i < 1280; i++)
            {
                Entity entity = World.Add();

                Transform transform = new Transform();
                transform.x = (i % 40) * 32;
                transform.y = (i / 40) * 32;
                transform.rotation = i;

                World.AddComponent(entity, transform);

                if (i % 5 == 0)
                {
                    World.AddComponent(entity, new SpriteRenderer(ed));
                }
                if (i % 5 == 1)
                {
                    World.AddComponent(entity, new SpriteRenderer(disc));
                }
                if (i % 5 == 2)
                {
                    World.AddComponent(entity, new SpriteRenderer(harold));
                }
                if (i % 5 == 3)
                {
                    World.AddComponent(entity, new SpriteRenderer(gerald));
                }
                if (i % 5 == 4)
                {
                    World.AddComponent(entity, new SpriteRenderer(pillar));
                }
            }

            Console.WriteLine("Setup complete");

            World.AddSystem(new FPSCounterSystem());
        }
    }
}
