using Modulus2D.Entities;
using Modulus2D.Core;

namespace Modulus2D.Graphics
{
    public class SpriteSystem : EntitySystem
    {
        private EntityFilter filter;
        private ComponentStorage<TransformComponent> transformComponents;
        private ComponentStorage<SpriteRendererComponent> spriteRendererComponents;

        private SpriteBatch batch;

        public SpriteSystem(SpriteBatch batch)
        {
            this.batch = batch;
        }

        public override void OnAdded()
        {
            transformComponents = World.GetStorage<TransformComponent>();
            spriteRendererComponents = World.GetStorage<SpriteRendererComponent>();
            
            filter = new EntityFilter(transformComponents, spriteRendererComponents);
        }

        public override void Render(float deltaTime)
        {            
            foreach (int id in World.Iterate(filter))
            {
                TransformComponent transform = transformComponents.Get(id);
                SpriteRendererComponent sprites = spriteRendererComponents.Get(id);

                for (int i = 0; i < sprites.sprites.Count; i++)
                {
                    batch.Draw(sprites.sprites[i].texture, transform.Position, transform.Rotation);
                }
            }
        }
    }
}