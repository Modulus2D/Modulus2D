using FarseerPhysics.Dynamics;
using Prota2D.Entities;
using Prota2D.Graphics;
using SFML.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Core
{
    public class Scene
    {
        // World
        public EntityWorld world = new EntityWorld();
        public PhysicsWorld physics = new PhysicsWorld();
        private SpriteSystem spriteSystem;

        public void Load(RenderWindow window)
        {
            spriteSystem = new SpriteSystem(window);
            world.AddSystem(spriteSystem);
        }

        public void Update(float deltaTime)
        {
            world.Update(deltaTime);
            physics.Update();
        }

        public void Render()
        {

        }
    }
}
