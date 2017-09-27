using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class EntityIterator : IEnumerable
    {
        private int last;
        private EntityWorld world;
        private EntityFilter filter;
        private List<IComponentStorage> storages = new List<IComponentStorage>();
        private Components components = new Components();

        public EntityIterator(int lastIndex, EntityWorld entityWorld, EntityFilter entityFilter)
        {
            last = lastIndex;
            world = entityWorld;
            filter = entityFilter;

            for (int i = 0; i < filter.components.Count; i++)
            {
                storages.Add(world.GetGenericStorage(filter.components[i]));
                components.Allocate();
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int id = 0; id < last; id++)
            {
                bool pass = true;

                for (int i = 0; i < storages.Count; i++)
                {
                    IComponentStorage storage = storages[i];

                    if(!storage.Has(id))
                    {
                        pass = false;
                        break;
                    } else
                    {
                        components.components[i] = storage.Get(id);
                    }
                }

                if (pass)
                {
                    components.ResetIndex();
                    yield return components;
                }
            }

            yield break;
        }
    }
}
