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
    public class Game
    {
        // Window
        private Window window;

        // Scenes
        public List<Scene> scenes = new List<Scene>();

        // Time
        private Clock clock = new Clock();

        public Window Window { get => window; set => window = value; }

        public Game(uint width, uint height, string name)
        {
            Window = new Window(new RenderWindow(new SFML.Window.VideoMode(width, height), name));
        }

        public void Start()
        {
            Window.RenderWindow.SetActive();
            Window.RenderWindow.Closed += new EventHandler(OnClosed);
            
            while (Window.RenderWindow.IsOpen)
            {
                Window.RenderWindow.DispatchEvents();

                Window.RenderWindow.Clear(new Color(0, 0, 0));

                Update();

                Window.RenderWindow.Display();
            }
        }

        public void Load(Scene scene)
        {
            scene.Load(Window);
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
