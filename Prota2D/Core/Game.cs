using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Graphics;
using Prota2D.Entities;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Prota2D.Core
{
    public class Game
    {
        // Window
        private RenderWindow window;

        // Scenes
        public List<Scene> scenes = new List<Scene>();

        // Time
        private Clock clock = new Clock();

        // Graphics
        private TextureLoader textures = new TextureLoader();

        public TextureLoader Textures { get => textures; }

        public Game(uint width, uint height, string name)
        {
            window = new RenderWindow(new VideoMode(width, height), name);
        }

        public void Start()
        {
            window.SetActive();
            window.Closed += new EventHandler(OnClosed);
            
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
            Window window = (Window)sender;
            window.Close();
        }
    }
}
