using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class Components
    {
        public List<IComponent> components = new List<IComponent>();
        private int i = 0;
        
        public void Allocate()
        {
            components.Add(null);
        }

        public void ResetIndex()
        {
            i = 0;
        }

        public T Next<T>() where T : IComponent
        {
            i++;
            return (T)components[(i-1)];
        }
    }
}
