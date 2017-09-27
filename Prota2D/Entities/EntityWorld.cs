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
        private Dictionary<Type, IComponentStorage> storages = new Dictionary<Type, IComponentStorage>();

        public Entity Create()
        {
            return new Entity(allocator.Create(), this);
        }

        public void Destroy(int id)
        {
            allocator.Remove(id);
            foreach (IComponentStorage storage in storages.Values)
            {
                storage.Clear(id);
            }
        }

        public T GetComponent<T>(int id) where T : IComponent
        {
            return GetStorage<T>().list[id];
        }

        public void RemoveComponent<T>(int id) where T : IComponent
        {
            GetStorage<T>().Clear(id);
        }

        public void AddComponent<T>(int id, T component) where T : IComponent
        {
            ComponentStorage<T> storage = GetStorage<T>();

            if (storage.list.Count < id + 1)
            {
                for (int i = 0; i < (id - storage.list.Count); i++)
                {
                    storage.list.Add(default(T));
                }

                storage.list.Add(component);
            }
            else
            {
                storage.list[id] = component;
            }
        }

        public ComponentStorage<T> GetStorage<T>() where T : IComponent
        {
            if (storages.TryGetValue(typeof(T), out IComponentStorage storage))
            {
                return (ComponentStorage<T>)storage;
            }
            else
            {
                ComponentStorage<T> newStorage = new ComponentStorage<T>();
                storages.Add(typeof(T), newStorage);
                return newStorage;
            }
        }

        public IComponentStorage GetGenericStorage(Type type)
        {
            if (storages.TryGetValue(type, out IComponentStorage storage))
            {
                return storage;
            }
            else
            {
                Type args = typeof(ComponentStorage<>).MakeGenericType(type);
                IComponentStorage newStorage = (IComponentStorage)Activator.CreateInstance(args);
                storages.Add(type, newStorage);
                return newStorage;
            }
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
