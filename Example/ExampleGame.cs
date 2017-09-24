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
            Scene scene = new Scene();

            for (int i = 0; i < 100; i++)
            {
                Entity entity = scene.world.Add();
                scene.world.AddComponent(entity, new Transform());
                scene.world.AddComponent(entity, new SpriteRenderer("Textures/Face.png"));
            }
            
            Console.WriteLine("Setup complete");

            scene.world.AddSystem(new FPSCounterSystem());

            SetScene(scene);
        }
    }
}
