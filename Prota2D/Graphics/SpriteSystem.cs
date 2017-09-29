using SFML.System;
using Prota2D.Entities;
using SFML.Graphics;

namespace Prota2D.Graphics
{
    public class SpriteSystem : EntitySystem
    {
        RenderWindow window;

        private EntityFilter filter = new EntityFilter();
        private View view;

        public SpriteSystem(RenderWindow renderWindow)
        {
            window = renderWindow;
            
            filter.Add<Core.Transform>();
            filter.Add<SpriteRenderer>();

            view = new View(new FloatRect(0f, 0f, 10f, 10f * window.Size.Y / window.Size.X));
        }
        
        public override void Update(EntityWorld world, float deltaTime)
        {
            window.SetView(view);
            //view.Center = new Vector2f(0f, 0f);

            foreach (Components components in world.Iterate(filter))
            {
                Core.Transform transform = components.Next<Core.Transform>();
                Sprite sprite = components.Next<SpriteRenderer>().sprite;

                sprite.Position = new Vector2f(transform.Position.X, transform.Position.Y);
                sprite.Scale = new Vector2f(transform.Scale.X / 32f, transform.Scale.Y / 32f);
                sprite.Rotation = transform.Rotation;

                window.Draw(sprite);
            }
        }
    }
}
