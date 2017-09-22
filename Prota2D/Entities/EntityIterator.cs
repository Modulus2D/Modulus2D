using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class EntityIterator : System.Collections.IEnumerable
    {
        private List<Entity> entities;
        private EntityWorld world;
        private EntityFilter filter;

        public EntityIterator(List<Entity> entityList, EntityWorld entityWorld, EntityFilter entityFilter)
        {
            entities = entityList;
            world = entityWorld;
            filter = entityFilter;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Entity entity in entities)
            {
                bool pass = true;

                foreach (Type type in filter.types)
                {
                    if (!world.HasComponentType(entity, type))
                    {
                        pass = false;
                        break;
                    }
                }

                if (pass)
                {
                    yield return entity;
                }
            }

            yield break;
        }
    }
}
