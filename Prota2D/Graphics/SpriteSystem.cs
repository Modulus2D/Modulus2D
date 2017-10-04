using SFML.System;
using Prota2D.Entities;
using Microsoft.Xna.Framework;
using System;
using Prota2D.Core;

namespace Prota2D.Graphics
{
    public class SpriteSystem : EntitySystem
    {
        private Window window;
        private EntityFilter filter = new EntityFilter();
        private SpriteBatch batch;
        private Vector2f pos = new Vector2f(2f, 2f);
        private float rot = 0f;

        private SFML.Graphics.Texture face = new SFML.Graphics.Texture("Resources/Textures/Face.png");
        private SFML.Graphics.Texture test = new SFML.Graphics.Texture("Resources/Textures/Test.png");

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

            batch.End();
        }
    }
}
