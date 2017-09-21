using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Entities;
using SFML.Graphics;

namespace Prota2D
{
    public class Scene
    {
        private EntitySystem entitySystem = new EntitySystem();

        public void Activate(RenderWindow window) {
            entitySystem.RegisterComponent<Graphics.Sprite>();
            Entity e1 = entitySystem.Add();
            entitySystem.AddComponent(e1, new Graphics.Sprite("A"));
            entitySystem.GetComponent<Graphics.Sprite>(entitySystem.Add());
        }

        public void Deactivate()
        {
        }

        public void Update()
        {
        }
    }
}
