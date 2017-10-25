using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    public class EntityFilter
    {
        private List<IComponentStorage> storages;

        public List<IComponentStorage> Storages { get => storages; set => storages = value; }

        public EntityFilter()
        {
            Storages = new List<IComponentStorage>();
        }

        public EntityFilter(params IComponentStorage[] storages)
        {
            Storages = storages.ToList();
        }

        public void Add(IComponentStorage storage)
        {
            Storages.Add(storage);
        }
    }
}
