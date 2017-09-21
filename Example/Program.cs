using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D;
using Prota2D.Graphics;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(1336, 768, "Example");
            Scene scene = new Scene();
            game.Scene = scene;

            // entity.AddComponent(new Transform());
            //entity.AddComponent(new Transform());
            //entity.AddComponent(new Sprite("Textures/Face.png"));

            game.Start();
        }
    }
}
