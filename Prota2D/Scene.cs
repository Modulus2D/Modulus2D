using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Entities;
using SFML.Graphics;
using Prota2D.Graphics;

namespace Prota2D
{
    public class Scene
    {
        public EntityWorld world = new EntityWorld();
        private SpriteSystem spriteSystem;

        public void Activate(RenderWindow window)
        {
            spriteSystem = new SpriteSystem(window);
            world.AddSystem(spriteSystem);
        }

        public void Deactivate()
        {
        }

        public void Update()
        {
            world.Update();
        }
    }
}
