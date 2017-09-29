using Prota2D.Entities;
using Prota2D.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Core
{
    public class Transform : IComponent
    {
        private Vector2 position = Vector2.Zero;
        private Vector2 scale = Vector2.Unit;
        private float rotation = 0f;

        public Vector2 Position {
            get => position;
            set => position = value;
        }
        public Vector2 Scale { get => scale; set => scale = value; }
        public float Rotation { get => rotation; set => rotation = value; }
    }
}
