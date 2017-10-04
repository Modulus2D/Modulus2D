using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    public class ComponentStorage<T> : IComponentStorage where T : IComponent
    {
        public List<T> list = new List<T>();

        public bool Has(int id)
        {
            if (list.Count > id)
            {
                return list[id] != null;
            } else
            {
                return false;
            }
        }

        public void Clear(int id)
        {
            list[id] = default(T);
        }

        public IComponent Get(int id)
        {
            return list[id];
        }
    }
}
