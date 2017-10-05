using Modulus2D.Core;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            App game = new App(new ExampleGame());
            game.Start();
        }
    }
}
