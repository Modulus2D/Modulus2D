using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Graphics;
using Prota2D.Entities;
using SFML.Graphics;
using SFML.System;

namespace Prota2D.Core
{
    public abstract class Game
    {
        // Scenes
        public List<Scene> scenes = new List<Scene>();

        // Time
        private Clock clock = new Clock();

        // Graphics
        private RenderWindow window;
        private TextureLoader textures = new TextureLoader();

        public TextureLoader Textures { get => textures; }

        public abstract void Init();

        public void SetWindow(RenderWindow renderWindow)
        {
            window = renderWindow;
        }

        public void Load(Scene scene)
        {
            scene.Load(window);
            scenes.Add(scene);
        }

        public void Unload(Scene scene)
        {
            scenes.Remove(scene);
        }

        public void Update()
        {
            float dt = clock.ElapsedTime.AsSeconds();
            clock.Restart();

            // Update
            for (int i = 0; i < scenes.Count; i++)
            {
                scenes[i].Update(dt);
            }
        }

        public void Render()
        {
            // Render
            for (int i = 0; i < scenes.Count; i++)
            {
                scenes[i].Render();
            }
        }
    }
}
