using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Graphics;
using Prota2D.Entities;
using SFML.Graphics;
using SFML.System;

namespace Prota2D
{
    public abstract class Game
    {
        // World
        private EntityWorld world = new EntityWorld();

        // Time
        private Clock clock = new Clock();

        // Graphics
        private RenderWindow window;
        private SpriteSystem spriteSystem;
        private TextureLoader textures = new TextureLoader();

        public EntityWorld World { get => world; }
        public TextureLoader Textures { get => textures; }

        public abstract void Init();

        public void SetWindow(RenderWindow renderWindow)
        {
            window = renderWindow;
            
            spriteSystem = new SpriteSystem(window);
            world.AddSystem(spriteSystem);
        }

        public void Update()
        {
            float dt = clock.ElapsedTime.AsSeconds();
            clock.Restart();
            world.Update(dt);
        }
    }
}
