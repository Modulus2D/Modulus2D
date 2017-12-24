using Modulus2D.Entities;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.UI
{
    public class MenuComponent : IComponent
    {
        private List<Widget> widgets;

        public MenuComponent()
        {
            widgets = new List<Widget>();
        }

        public void Add(Widget widget)
        {
            widgets.Add(widget);
        }

        public void Draw(RenderTarget target)
        {
            for(int i = 0; i < widgets.Count; i++)
            {
                //widgets[i];
            }
        }
    }
}
