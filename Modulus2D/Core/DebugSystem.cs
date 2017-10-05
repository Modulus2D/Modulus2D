using Modulus2D.Core;
using Modulus2D.Entities;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Core
{
    public class DebugSystem : EntitySystem
    {
        private Map map;
        private float reloadTimer = 0f;

        public DebugSystem(Map map)
        {
            this.map = map;
        }

        public override void Update(float deltaTime)
        {
            reloadTimer += deltaTime;

            if (Keyboard.IsKeyPressed(Keyboard.Key.R) && reloadTimer > 1f)
            {
                Console.WriteLine("Reload");
                map.Reload();
                reloadTimer = 0f;
            }
        }
    }
}
