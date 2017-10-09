using Microsoft.Xna.Framework;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Physics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class PlayerSystem : EntitySystem
    {
        private float speed = 200f;
        private float jump = 1000f;

        private EntityFilter filter = new EntityFilter();

        public PlayerSystem()
        {
            filter.Add<PlayerComponent>();
            filter.Add<PhysicsComponent>();
        }

        public override void Update(float deltaTime)
        {
            foreach (Components components in World.Iterate(filter))
            {
                PlayerComponent player = components.Next<PlayerComponent>();
                PhysicsComponent physics = components.Next<PhysicsComponent>();
                
                float move = 0f;

                if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.D))
                {
                    move += 1f;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.A))
                {
                    move -= 1f;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Up) || Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Space) || Joystick.IsButtonPressed(0, 0))
                {
                    physics.Body.ApplyForce(new Vector2(0f, -jump * deltaTime));
                }

                physics.Body.ApplyForce(new Vector2(move * speed * deltaTime, 0f));
            }
        }
    }
}
