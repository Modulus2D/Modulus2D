using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class EntityFilter
    {
        public List<Type> types = new List<Type>();

        public EntityFilter(params Type[] componentTypes)
        {
            foreach (Type type in componentTypes)
            {
                types.Add(type);
            }
        }
    }
}
