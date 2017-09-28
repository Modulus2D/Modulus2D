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
using Prota2D.Physics;

namespace Prota2D.Core
{
    public class Scene
    {
        // World
        public EntityWorld world = new EntityWorld();
        public PhysicsWorld physics = new PhysicsWorld();
        private PhysicsSystem physicsSystem;
        private SpriteSystem spriteSystem;

        public void Load(RenderWindow window)
        {
            spriteSystem = new SpriteSystem(window);
            physicsSystem = new PhysicsSystem(physics);

            world.AddSystem(spriteSystem);
            world.AddSystem(physicsSystem);
        }

        public void Update(float deltaTime)
        {
            world.Update(deltaTime);
            physics.Update(deltaTime);
        }

        public void Render()
        {

        }
    }
}
