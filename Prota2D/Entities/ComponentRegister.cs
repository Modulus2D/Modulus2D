using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class ComponentRegister
    {
        private Dictionary<Type, object> dictionary = new Dictionary<Type, object>();

        public void Register<T>() where T : IComponent
        {
            dictionary[typeof(T)] = new Dictionary<int, T>();
        }

        public void Set<T>(int id, T component) where T : IComponent
        {
            var type = typeof(T);

            if (!dictionary.ContainsKey(type))
            {
                Register<T>();
            }

            ((Dictionary <int, T>)dictionary[type])[id] = component;
        }

        public bool Contains<T>(int id) where T : IComponent
        {
            return ContainsType(id, typeof(T));
        }

        public bool ContainsType(int id, Type type)
        {
            if (dictionary.ContainsKey(type))
            {
                return true;
            }

            return false;
        }

        public Dictionary<int, T> Get<T>() where T : IComponent
        {
            return (Dictionary<int, T>)dictionary[typeof(T)];
        }
    }
}
