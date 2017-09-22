using Prota2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D
{
    public class Transform : IComponent
    {
        public float x = 0f;
        public float y = 0f;
        public float scaleX = 1f;
        public float scaleY = 1f;
        public float rotation = 0f;
    }
}
