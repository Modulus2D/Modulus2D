using Modulus2D.Entities;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.UI
{
    public class UISystem : EntitySystem
    {
        EntityFilter filter = new EntityFilter();
        View view;
        RenderTarget target;

        public UISystem(RenderTarget target)
        {
            this.target = target;

            view = new View(new FloatRect(0f, 0f, target.Size.X, target.Size.Y));

            filter.Add<TextComponent>();
        }

        public override void Update(float deltaTime)
        {
            target.SetView(view);

            foreach(Components components in World.Iterate(filter))
            {
                TextComponent component = components.Next<TextComponent>();
                component.Draw(target);
            }
        }
    }
}
