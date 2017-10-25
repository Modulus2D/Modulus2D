using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    public class EntityIterator : IEnumerable
    {
        private int lastIndex;
        private EntityWorld world;
        private EntityFilter filter;

        public EntityIterator(int lastIndex, EntityWorld world, EntityFilter filter)
        {
            this.lastIndex = lastIndex;
            this.world = world;
            this.filter = filter;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < lastIndex; i++)
            {
                bool pass = true;

                for (int j = 0; j < filter.Storages.Count; j++)
                {
                    IComponentStorage storage = filter.Storages[j];

                    if(!storage.Has(i))
                    {
                        pass = false;
                        break;
                    }
                }

                if (pass)
                {
                    yield return i;
                }
            }

            yield break;
        }
    }
}
