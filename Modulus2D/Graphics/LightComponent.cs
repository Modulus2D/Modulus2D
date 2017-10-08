using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class LightComponent : IComponent
    {
        LightType type;

        public LightComponent(LightType type)
        {
            this.type = type;
        }
    }

    public enum LightType
    {
        Point
    }
}
