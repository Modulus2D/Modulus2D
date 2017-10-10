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
    public class TextComponent : IComponent
    {
        Text text;
        
        public TextComponent(Font font)
        {
            Text = new Text("", font)
            {
                Scale = new Vector2f(1f, 1f)
            };
        }

        public Text Text { get => text; set => text = value; }

        public void Draw(RenderTarget target)
        {
            target.Draw(Text);
        }
    }
}
