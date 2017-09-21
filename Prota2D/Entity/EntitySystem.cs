using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class EntitySystem
    {
        private int first = 0;
        public List<bool> entities = new List<bool>();
        private ComponentRegister register = new ComponentRegister();

        public Entity Add()
        {
            int id = first;

            // Expand list if necessary
            if (first > entities.Count - 1)
            {
                entities.Add(true);
                first = entities.Count;
            } else
            {
                entities[id] = true;
                first = FindNextOpen(id);
            }

            return new Entity(id);
        }
        
        public void Remove(Entity entity)
        {
            entities[entity.id] = false;
            
            if (entity.id < first)
            {
                first = entity.id;
            }
        }

        private int FindNextOpen(int id)
        {
            int ret = id;

            while (ret < entities.Count && entities[ret] == true)
            {
                ret += 1;
            }

            return ret;
        }

        public void RegisterComponent<T>() where T : IComponent
        {
            register.Register<T>();
        }

        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            return register.Get<T>()[entity.id];
        }

        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {
            register.Get<T>().Insert(entity.id, component);
        }
    }
}
