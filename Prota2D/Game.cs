using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Prota2D
{
    public class Game
    {
        private RenderWindow window;

        private Scene scene;
        public Scene Scene
        {
            get
            {
                return scene;
            }
            set
            {
                if(scene != null)
                {
                    scene.Deactivate();
                }
                scene = value;
                scene.Activate(window);
            }
        }

        public Game(uint width, uint height, string name)
        {
            // SFML
            window = new RenderWindow(new VideoMode(width, height), name);

            window.SetActive();
            window.Closed += new EventHandler(OnClosed);  
        }

        public void Start()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(new Color(0, 0, 0));

                if (scene != null)
                {
                    scene.Update();
                }

                window.Display();
            }
        }

        static void OnClosed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }
    }
}
