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

        public EntityIterator(int lastIndex, EntityWorld entityWorld, EntityFilter entityFilter)
        {
            last = lastIndex;
            world = entityWorld;
            filter = entityFilter;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < last; i++)
            {
                bool pass = true;

                foreach (IComponentFilter filter in filter.filters)
                {
                    if (!filter.Test(i))
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
