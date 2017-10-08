using Modulus2D.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start(new GameState(), "Example", 1336, 768);

            //Server server = new Server();
            //server.Start(new ServerState());
        }
    }
}