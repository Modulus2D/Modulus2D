/*using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class CameraSystem : EntitySystem
    {
        private OrthoCamera camera;
        public List<TransformComponent> targets = new List<TransformComponent>();

        private float lerp = 5f;

        public CameraSystem(OrthoCamera camera)
        {
            this.camera = camera;
        }

        public override void Update(float deltaTime)
        {
            if (targets.Count > 0)
            {
                camera.Position += (targets[0].Position - camera.Position) * lerp * deltaTime;
                camera.Update();
            }
        }
    }
}
*/