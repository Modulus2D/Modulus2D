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
   public class SpriteSystem : EntitySystem
    {
        SFML.Graphics.RenderWindow window;

        private EntityFilter filter = new EntityFilter();

        public SpriteSystem(SFML.Graphics.RenderWindow renderWindow)
        {
            window = renderWindow;
            
            filter.Add<Transform>();
            filter.Add<SpriteRenderer>();
        }
        
        public override void Update(EntityWorld world, float deltaTime)
        {
            foreach (Components components in world.Iterate(filter))
            {
                Transform transform = components.Next<Transform>();
                SFML.Graphics.Sprite sprite = components.Next<SpriteRenderer>().sprite;

                sprite.Position = new Vector2f(transform.Position.X, transform.Position.Y);
                sprite.Scale = new Vector2f(transform.Scale.X, transform.Scale.Y);
                sprite.Rotation = transform.Rotation;
                window.Draw(sprite);
            }
        }
    }
}
