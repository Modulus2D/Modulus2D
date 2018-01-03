using Modulus2D.Entities;
using Modulus2D.Core;

namespace Modulus2D.Graphics
{
    public class SpriteSystem : EntitySystem
    {
        private EntityFilter filter;
        private ComponentStorage<TransformComponent> transformComponents;
        private ComponentStorage<SpriteComponent> spriteRendererComponents;

        private SpriteBatch batch;

        public SpriteSystem(SpriteBatch batch)
        {
            this.batch = batch;
        }

        public override void OnAdded()
        {
            transformComponents = World.GetStorage<TransformComponent>();
            spriteRendererComponents = World.GetStorage<SpriteComponent>();
            
            filter = new EntityFilter(transformComponents, spriteRendererComponents);
        }

        public override void Render(float deltaTime)
        {            
            foreach (int id in World.Iterate(filter))
            {
                TransformComponent transform = transformComponents.Get(id);
                SpriteComponent sprites = spriteRendererComponents.Get(id);

                batch.Draw(sprites.Texture, transform.Position + sprites.Offset, transform.Rotation);
            }
        }
    }
}