using Modulus2D.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Headless server = new Headless();
            server.Start(new ServerState());
        }
    }
}
