using Prota2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public interface IEntitySystem
    {
        void Update(EntityWorld world, float deltaTime);
    }
}
