using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prota2D.Graphics;

namespace Prota2D.Entities
{
    public class EntityWorld
    {
        private EntityAllocator allocator = new EntityAllocator();
        private List<IEntitySystem> systems = new List<IEntitySystem>();

        public Entity Add()
        {
            return new Entity(allocator.Create());
        }

        public void Remove(Entity entity)
        {
            allocator.Remove(entity.id);
        }

        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            return ComponentLists.Get<T>()[entity.id];
        }

        public T GetComponent<T>(int id) where T : IComponent
        {
            return ComponentLists.Get<T>()[id];
        }

        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {
            List<T> list = ComponentLists.Get<T>();
            int id = entity.id;

            if (list.Count < id + 1)
            {
                
                for (int i = 0; i < (id - list.Count); i++)
                {
                    Console.WriteLine("Spacing...");
                    list.Add(default(T));
                }

                list.Add(component);
            } else
            {
                list[id] = component;
            }
        }

        public bool HasComponent<T>(Entity entity) where T : IComponent
        {
            return ComponentLists.Get<T>()[entity.id] != null;
        }

        public bool HasComponent<T>(int id) where T : IComponent
        {
            return ComponentLists.Get<T>()[id] != null;
        }

        public void Update(float deltaTime)
        {
            foreach (IEntitySystem system in systems)
            {
                system.Update(this, deltaTime);
            }
        }

        public void AddSystem(IEntitySystem system)
        {
            systems.Add(system);
        }

        public EntityIterator Iterate(EntityFilter filter)
        {
            return new EntityIterator(allocator.GetLast(), this, filter);
        }
    }
}
