using SFML.System;
using Modulus2D.Entities;
using Microsoft.Xna.Framework;
using System;
using Modulus2D.Core;
using SFML.Graphics;

namespace Modulus2D.Graphics
{
    public class SpriteSystem : EntitySystem
    {
        private EntityFilter filter = new EntityFilter();
        private SpriteBatch batch;

        public SpriteSystem(SpriteBatch batch)
        {
            this.batch = batch;

            filter.Add<TransformComponent>();
            filter.Add<SpriteComponent>();
        }
        
        public override void Update(float deltaTime)
        {
            batch.Begin();

            foreach (Components components in World.Iterate(filter))
            {
                TransformComponent transform = components.Next<TransformComponent>();
                batch.Draw(components.Next<SpriteComponent>().Texture, transform.Position, transform.Rotation);
            }

            batch.End();
        }
    }
}
