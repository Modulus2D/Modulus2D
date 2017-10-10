using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Map;
using Modulus2D.UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Core
{
    public class DebugSystem : EntitySystem
    {
        private float reloadTimer = 0f;
        private MapSystem mapSystem;
        private TextComponent text;

        public DebugSystem(MapSystem mapSystem)
        {
            this.mapSystem = mapSystem;

            Entity textEntity = World.Create();

            Font font = new Font("Resources/Fonts/Inconsolata-Regular.ttf");
            text = new TextComponent(font);
            textEntity.AddComponent(text);
        }

        public override void Update(float deltaTime)
        {
            reloadTimer += deltaTime;

            // Disabled until console is implemented            
            /*if (Keyboard.IsKeyPressed(Keyboard.Key.R) && reloadTimer > 1f)
            {
                Console.WriteLine("Reload");
                mapSystem.ReloadAll();
                reloadTimer = 0f;
            }*/
        }
    }
}
