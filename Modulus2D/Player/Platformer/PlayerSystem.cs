using Microsoft.Xna.Framework;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Physics;
using Modulus2D.Core;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Player.Platformer
{
    /// <summary>
    /// Basic platformer player system
    /// </summary>
    public class PlayerSystem : EntitySystem
    {
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
                
                if (player.Jump)
                {
                    physics.Body.ApplyForce(new Vector2(0f, -player.JumpForce * deltaTime));
                }

                physics.Body.ApplyForce(new Vector2(player.MoveX * player.MoveForce * deltaTime, 0f));
            }
        }
    }
}
