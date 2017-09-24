using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using Prota2D.Entities;

namespace Prota2D.Graphics
{
   public class SpriteSystem : IEntitySystem
    {
        RenderWindow window;

        private EntityFilter filter;

        public SpriteSystem(RenderWindow renderWindow)
        {
            window = renderWindow;

            filter = new EntityFilter();
            filter.Add<Transform>();
            //filter.Add<SpriteRenderer>();
        }
        
        public void Update(EntityWorld world, float deltaTime)
        {
            foreach (int id in world.Iterate(filter))
            {
                Console.Write("D");

                Transform transform = world.GetComponent<Transform>(id);
                /*Sprite sprite = world.GetComponent<SpriteRenderer>(id).sprite;

                sprite.Position = new Vector2f(transform.x, transform.y);
                sprite.Scale = new Vector2f(transform.scaleX, transform.scaleY);
                sprite.Rotation = transform.rotation;
                window.Draw(sprite);*/
            }
        }
    }
}
