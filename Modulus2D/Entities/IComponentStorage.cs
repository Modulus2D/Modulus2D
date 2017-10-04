using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    public interface IComponentStorage
    {
        bool Has(int id);
        IComponent Get(int id);
        void Clear(int id);
    }
}
