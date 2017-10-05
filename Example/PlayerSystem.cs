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
    class PlayerSystem : EntitySystem
    {
        private OrthoCamera camera;
        private Entity player;
        private PhysicsComponent physics;
        private float speed = 200f;
        private float jump = 1000f;

        public PlayerSystem(OrthoCamera camera, Entity player)
        {
            this.camera = camera;
            this.player = player;

            physics = player.GetComponent<PhysicsComponent>();
        }

        public override void Update(float deltaTime)
        {
            camera.Position = physics.Body.Position;

            float move = 0f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                move += 1f;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                move -= 1f;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                physics.Body.ApplyForce(new Vector2(0f, -jump * deltaTime));
            }

            physics.Body.ApplyForce(new Vector2(move * speed * deltaTime, 0f));
        }
    }
}
