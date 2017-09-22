using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using Prota2D.Graphics;

namespace Prota2D
{
    public class Application
    {
        private RenderWindow window;
        private Game game;

        public Application(Game appGame, uint width, uint height, string name)
        {
            // SFML
            window = new RenderWindow(new VideoMode(width, height), name);

            game = appGame;
        }

        public void Start()
        {
            window.SetActive();
            window.Closed += new EventHandler(OnClosed);

            game.SetWindow(window);
            game.Init();

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(new Color(0, 0, 0));

                game.Update();

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
