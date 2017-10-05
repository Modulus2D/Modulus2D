using Modulus2D.Core;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Core
{
    public interface IGame
    {
        void Start(App app);
        void Update(float deltaTime, RenderTarget target, Scene scene);
        Config Config();
    }
}
