using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Core;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Application application = new Application(new ExampleGame(), 1280, 768, "Example");
            application.Start();
        }
    }
}
