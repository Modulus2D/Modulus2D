using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    class ComponentRegister
    {
        private Dictionary<Type, object> dictionary = new Dictionary<Type, object>();

        public void Register<T>() where T : IComponent
        {
            dictionary[typeof(T)] = new List<T>();
        }

        public List<T> Get<T>() where T : IComponent
        {
            return (List<T>)dictionary[typeof(T)];
        }
    }
}
