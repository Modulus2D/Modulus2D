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

        public SpriteSystem(RenderWindow renderWindow)
        {
            window = renderWindow;
        }

        public void Update(EntityWorld world)
        {
            foreach(Entity entity in world.Iterate(new EntityFilter(typeof(Sprite), typeof(Transform))))
            {
               /* Transform transform = world.GetComponent<Transform>(entity);
                transform.rotation += 0.2f;

                SFML.Graphics.Sprite sprite = world.GetComponent<Sprite>(entity).sprite;
                sprite.Position = new Vector2f(transform.x, transform.y);
                sprite.Scale = new Vector2f(transform.scaleX, transform.scaleY);
                sprite.Rotation = transform.rotation;

                //window.Draw(sprite);*/
            }
        }
    }
}
