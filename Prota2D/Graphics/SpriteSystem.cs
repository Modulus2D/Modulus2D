using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using Prota2D.Core;
using Prota2D.Entities;

namespace Prota2D.Graphics
{
   public class SpriteSystem : IEntitySystem
    {
        SFML.Graphics.RenderWindow window;

        private EntityFilter filter;

        public SpriteSystem(SFML.Graphics.RenderWindow renderWindow)
        {
            window = renderWindow;

            filter = new EntityFilter();
            filter.Add<Transform>();
            filter.Add<SpriteRenderer>();
        }
        
        public void Update(EntityWorld world, float deltaTime)
        {
            foreach (Components components in world.Iterate(filter))
            {
                Transform transform = components.Next<Transform>();
                SFML.Graphics.Sprite sprite = components.Next<SpriteRenderer>().sprite;

                sprite.Position = new Vector2f(transform.position.X, transform.position.Y);
                sprite.Scale = new Vector2f(transform.scale.X, transform.scale.Y);
                sprite.Rotation = transform.rotation;
                window.Draw(sprite);
            }
        }
    }
}
