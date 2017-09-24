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
        private Scene currentScene;
        private RenderWindow window;
        private Clock clock = new Clock();

        public abstract void Init();

        public void SetWindow(RenderWindow renderWindow)
        {
            window = renderWindow;
        }

        public void SetScene(Scene scene)
        {
            if (currentScene != null)
            {
                currentScene.Deactivate();
            }

            currentScene = scene;
            currentScene.Activate(window);
        }

        public void Update()
        {
            float dt = clock.ElapsedTime.AsSeconds();
            clock.Restart();
            currentScene.Update(dt);
        }
    }
}
