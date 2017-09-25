using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    class EntityAllocator
    {
        private int nextId = 0;
        private List<int> recycled = new List<int>();

        public int Create()
        {
            // Recycle entities if possible
            if (recycled.Count > 0)
            {
                int id = recycled[recycled.Count - 1];
                recycled.RemoveAt(recycled.Count - 1);

                return id;
            }

            // Otherwise return next id
            nextId += 1;
            return nextId - 1;
        }

        public void Remove(int id)
        {
            recycled.Add(id);
        }

        public int GetLast()
        {
            return nextId;
        }
    }
}
