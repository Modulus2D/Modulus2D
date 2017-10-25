using Microsoft.Xna.Framework;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class LightSystem : EntitySystem
    {
        /*private RenderTarget target;
        private EntityFilter filter = new EntityFilter();

        private Texture pointLight = new Texture("Resources/Textures/PointLight.png");
        private SpriteBatch batch;

        public LightSystem(RenderTarget target)
        {
            this.target = target;

            batch = new SpriteBatch(target);

            filter.Add<TransformComponent>();
            filter.Add<LightComponent>();
        }

        public override void Update(float deltaTime)
        {
            batch.Begin();

            foreach (Components components in World.Iterate(filter))
            {
                TransformComponent transform = components.Next<TransformComponent>();
                LightComponent light = components.Next<LightComponent>();
                
                batch.Draw(pointLight, transform.Position, new Vector2(0.3f, 0.3f));
            }

            batch.End();
        }*/
    }
}
