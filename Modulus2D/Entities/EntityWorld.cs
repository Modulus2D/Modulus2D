using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modulus2D.Graphics;

namespace Modulus2D.Entities
{

    public class EntityWorld
    {
        private EntityAllocator allocator = new EntityAllocator();
        private List<EntitySystem> systems = new List<EntitySystem>();
        private Dictionary<Type, IComponentStorage> storages = new Dictionary<Type, IComponentStorage>();
        private Dictionary<Type, List<Action<Entity>>> entityListeners = new Dictionary<Type, List<Action<Entity>>>();

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

        public bool HasComponent<T>(int id) where T : IComponent
        {
            return GetStorage<T>().list[id] != null;
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
                // Add to end of list
                while (id + 1 > storage.list.Count)
                {
                    storage.list.Add(default(T));
                }
                
                storage.list[id] = component;
            }
            else
            {
                // Add in list
                storage.list[id] = component;
            }

            List<Action<Entity>> listeners = GetListeners<T>();
            if(listeners.Count > 0)
            {
                Entity entity = new Entity(id, this);
                foreach (Action<Entity> listener in listeners)
                {
                    listener(entity);
                }
            }
        }

        public void AddListener<T>(Action<Entity> action) where T : IComponent
        {
            GetListeners<T>().Add(action);
        }

        private List<Action<Entity>> GetListeners<T>() where T : IComponent
        {
            if (entityListeners.TryGetValue(typeof(T), out List<Action<Entity>> listeners))
            {
                return listeners;
            }
            else
            {
                List<Action<Entity>> newListeners = new List<Action<Entity>>();
                entityListeners.Add(typeof(T), newListeners);
                return newListeners;
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
            foreach (EntitySystem system in systems)
            {
                system.Update(this, deltaTime);
            }
        }

        public void AddSystem(EntitySystem system)
        {
            system.Init(this);
            systems.Add(system);
        }

        public EntityIterator Iterate(EntityFilter filter)
        {
            return new EntityIterator(allocator.GetLast(), this, filter);
        }
    }
}
