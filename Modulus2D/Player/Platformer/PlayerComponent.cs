using Modulus2D.Entities;
using Modulus2D.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Player.Platformer
{
    /// <summary>
    /// Basic platformer player component
    /// </summary>
    public class PlayerComponent : IComponent, ITransmit, IReceive
    {
        private float moveForce = 200f;
        private float jumpForce = 1000f;

        private float moveX;
        private bool jump;

        public float MoveForce { get => moveForce; set => moveForce = value; }
        public float JumpForce { get => jumpForce; set => jumpForce = value; }
        public float MoveX { get => moveX; set => moveX = value; }
        public bool Jump { get => jump; set => jump = value; }
        
        public IUpdate Send()
        {
            return new PlayerUpdate()
            {
                moveX = moveX,
                jump = jump
            };
        }

        public void Receive(IUpdate update)
        {
            PlayerUpdate playerUpdate = (PlayerUpdate)update;

            // TODO: Check if value is reasonable
            moveX = playerUpdate.moveX;
            jump = playerUpdate.jump;
        }
    }

    [Serializable]
    class PlayerUpdate : IUpdate
    {
        public float moveX;
        public bool jump;
    }
}
