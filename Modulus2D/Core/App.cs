using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modulus2D.Graphics;
using Modulus2D.Entities;
using SFML.Graphics;
using SFML.System;
using Prota2D.Core;

namespace Modulus2D.Core
{
    public class App
    {
        // Window
        private RenderWindow window;

        // Scenes
        private List<Scene> scenes = new List<Scene>();

        // Time
        private Clock clock = new Clock();

        // Game listener
        private IGame game;

        public App(IGame game)
        {
            this.game = game;

            Config config = game.Config();

            window = new RenderWindow(new SFML.Window.VideoMode(config.Width, config.Height), config.Name);
        }

        public void Start()
        {
            window.SetActive();
            window.Closed += new EventHandler(OnClosed);

            game.Start(this);
            
            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(new Color(0, 0, 0));

                Update();

                window.Display();
            }
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

            game.Update(dt, window, scenes[0]);

            // Update
            for (int i = 0; i < scenes.Count; i++)
            {
                scenes[i].Update(dt);
            }
        }

        // TODO: Create separate render loop?
        /*public void Render()
        {
            // Render
            for (int i = 0; i < scenes.Count; i++)
            {
                scenes[i].Render();
            }
        }*/

        static void OnClosed(object sender, EventArgs e)
        {
            SFML.Window.Window window = (SFML.Window.Window)sender;
            window.Close();
        }
    }
}
