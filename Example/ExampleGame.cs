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

            for (int i = 0; i < 50000; i++)
            {
                Entity entity = scene.world.Add();
                scene.world.AddComponent(entity, new Transform());
                scene.world.AddComponent(entity, new Sprite("Textures/Face.png"));
                scene.world.GetComponent<Transform>(entity).rotation = 180f;
                scene.world.GetComponent<Transform>(entity).x = 180f;
                scene.world.GetComponent<Transform>(entity).y = 180f;
            }


            SetScene(scene);
        }
    }
}
