using Modulus2D.Core;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start(new GameState(), "Example", 1336, 768);
        }
    }
}
