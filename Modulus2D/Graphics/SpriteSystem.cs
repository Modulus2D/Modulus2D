using SFML.System;
using Modulus2D.Entities;
using Microsoft.Xna.Framework;
using System;
using Modulus2D.Core;

namespace Modulus2D.Graphics
{
    public class SpriteSystem : EntitySystem
    {
        private Window window;
        private EntityFilter filter = new EntityFilter();
        private SpriteBatch batch;

        Map map = new Map("Resources/Maps/Test.tmx");

        public SpriteSystem(Window window)
        {
            this.window = window;

            batch = new SpriteBatch(window);

            filter.Add<Transform>();
            filter.Add<SpriteRenderer>();
        }
        
        public override void Update(EntityWorld world, float deltaTime)
        {
            batch.Begin();

            foreach (Components components in world.Iterate(filter))
            {
                Transform transform = components.Next<Transform>();
                batch.Draw(components.Next<SpriteRenderer>().Texture, transform.Position, transform.Rotation);
            }

            map.Draw(batch);

            batch.End();
        }
    }
}
