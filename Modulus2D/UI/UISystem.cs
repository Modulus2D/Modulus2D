using Modulus2D.Core;
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
        private EntityFilter filter;
        private ComponentStorage<MenuComponent> menuComponents;

        private View view;
        private RenderTarget target;

        public UISystem(RenderTarget target)
        {
            this.target = target;
            view = new View(new FloatRect(0f, 0f, target.Size.X, target.Size.Y));
        }

        public override void OnAdded()
        {
            menuComponents = World.GetStorage<MenuComponent>();

            filter = new EntityFilter(menuComponents);
        }

        public override void Update(float deltaTime)
        {
            target.SetView(view);

            foreach(int id in World.Iterate(filter))
            {
                MenuComponent menu = menuComponents.Get(id);

                menu.Draw(target);
            }
        }
    }
}
