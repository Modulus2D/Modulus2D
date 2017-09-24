using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class EntityFilter
    {
        public List<IComponentFilter> filters = new List<IComponentFilter>();

        public void Add<T>() where T : IComponent
        {
            filters.Add(new ComponentFilter<T>());
        }
    }
}
