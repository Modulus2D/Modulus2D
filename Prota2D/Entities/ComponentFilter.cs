using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    class ComponentFilter<T> : IComponentFilter where T : IComponent
    {
        List<T> list = ComponentLists.Get<T>();

        public bool Test(int id)
        {
            return id < list.Count && list[id] != null;
        }
    }
}
