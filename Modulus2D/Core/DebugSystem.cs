using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Map;
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

        public DebugSystem(MapSystem mapSystem)
        {
            this.mapSystem = mapSystem;
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
