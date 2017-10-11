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
using Modulus2D.Input;

namespace Modulus2D.Player.Platformer
{
    /// <summary>
    /// Receives input for players
    /// </summary>
    public class PlayerInputSystem : EntitySystem
    {
        private InputValue moveX;
        private InputValue jump;

        private EntityFilter filter = new EntityFilter();

        public PlayerInputSystem(InputValue moveX, InputValue jump)
        {
            this.moveX = moveX;
            this.jump = jump;

            filter.Add<PlayerComponent>();
            filter.Add<PlayerInputComponent>();
        }

        public override void Update(float deltaTime)
        {
            foreach (Components components in World.Iterate(filter))
            {
                PlayerComponent player = components.Next<PlayerComponent>();
                PlayerInputComponent input = components.Next<PlayerInputComponent>();

                player.MoveX = moveX.Value;

                if (jump.Value > 0f)
                {
                    player.Jump = true;
                } else
                {
                    player.Jump = false;
                }
            }
        }
    }
}
