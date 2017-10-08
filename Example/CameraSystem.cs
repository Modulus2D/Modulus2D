using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class CameraSystem : EntitySystem
    {
        OrthoCamera camera;
        TransformComponent target;

        public CameraSystem(OrthoCamera camera, TransformComponent target)
        {
            this.camera = camera;
            this.target = target;
        }

        public override void Update(float deltaTime)
        {
            camera.Position = target.Position;
            camera.Update();
        }
    }
}
