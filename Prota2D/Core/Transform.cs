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
        public Vector2 position = Vector2.Zero;
        public Vector2 scale = Vector2.Unit;
        public float rotation = 0f;
    }
}
