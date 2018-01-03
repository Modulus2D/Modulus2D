using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Input;
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

namespace Modulus2D.Utility
{
    public class DebugSystem : EntitySystem
    {
        private MapSystem mapSystem;
        // private TextComponent text;
        private OneShotInput reload;

        public DebugSystem(MapSystem mapSystem, OneShotInput reload)
        {
            this.mapSystem = mapSystem;
            this.reload = reload;

            // Move this to OnAdded()
            // Entity textEntity = World.Create();

            // Font font = new Font("Resources/Fonts/Inconsolata-Regular.ttf");
            // text = new TextComponent(font);
            // textEntity.AddComponent(text);
        }
        
        public override void Update(float deltaTime)
        {
            // Disabled until console is implemented
            if (reload.Active)
            {
                Console.WriteLine("Reload");
                mapSystem.ReloadAll();
            }
        }
    }
}
