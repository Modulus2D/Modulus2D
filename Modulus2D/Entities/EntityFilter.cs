using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    public class EntityFilter
    {
        public List<Type> components = new List<Type>();

        public void Add<T>() where T : IComponent
        {
            components.Add(typeof(T));
        }

        public void Add(Type t)
        {
            components.Add(t);
        }
    }
}
