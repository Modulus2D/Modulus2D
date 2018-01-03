using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modulus2D.Graphics;

namespace Modulus2D.Entities
{
    public delegate void ComponentAdded(Entity entity);
    public delegate void ComponentRemoved(Entity entity);

    public class EntityWorld
    {
        private EntityAllocator allocator;

        private List<EntitySystem> systems;

        private Dictionary<Type, IComponentStorage> storages;

        private Dictionary<Type, List<ComponentAdded>> componentAddedListeners;
        private Dictionary<IComponentStorage, List<ComponentRemoved>> componentRemovedListeners;

        public EntityWorld()
        {
            allocator = new EntityAllocator();

            systems = new List<EntitySystem>();

            storages = new Dictionary<Type, IComponentStorage>();

            componentAddedListeners = new Dictionary<Type, List<ComponentAdded>>();
            componentRemovedListeners = new Dictionary<IComponentStorage, List<ComponentRemoved>>();
        }

        public Entity Create()
        {
            return new Entity(allocator.Create(), this);
        }

        public void Remove(int id)
        {
            allocator.Remove(id);

            List<IComponentStorage> toClear = new List<IComponentStorage>();

            foreach (IComponentStorage storage in storages.Values)
            {
                // Notify listeners
                if (storage.Has(id))
                {
                    List<ComponentRemoved> listeners = GetRemovedListeners(storage);
                    if (listeners.Count > 0)
                    {
                        Entity entity = new Entity(id, this);
                        foreach (ComponentRemoved listener in listeners)
                        {
                            listener(entity);
                        }
                    }

                    toClear.Add(storage);
                }
            }

            // Entity must remain intact until all listeners are called
            for (int i = 0; i < toClear.Count; i++)
            {
                toClear[i].Clear(id);
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
            
            // Add intermediate items if necessary
            if (storage.list.Count < id + 1)
            {
                // Add to end of list
                while (id + 1 > storage.list.Count)
                {
                    storage.list.Add(default(T));
                }
            }

            Entity entity = new Entity(id, this);

            if (storage.list[id] != null)
            {
                List<ComponentRemoved> removedListeners = GetRemovedListeners(storage);

                if (removedListeners.Count > 0)
                {
                    foreach (ComponentRemoved listener in removedListeners)
                    {
                        listener(entity);
                    }
                }
            }

            storage.list[id] = component;
            
            List<ComponentAdded> addListeners = GetAddedListeners<T>();
            if(addListeners.Count > 0)
            {
                foreach (ComponentAdded listener in addListeners)
                {
                    listener(entity);
                }
            }
        }

        public void AddCreatedListener<T>(ComponentAdded listener) where T : IComponent
        {
            GetAddedListeners<T>().Add(listener);
        }

        private List<ComponentAdded> GetAddedListeners<T>() where T : IComponent
        {
            if (componentAddedListeners.TryGetValue(typeof(T), out List<ComponentAdded> listeners))
            {
                return listeners;
            }
            else
            {
                List<ComponentAdded> newListeners = new List<ComponentAdded>();
                componentAddedListeners.Add(typeof(T), newListeners);
                return newListeners;
            }
        }

        public void AddRemovedListener<T>(ComponentRemoved listener) where T : IComponent
        {
            GetRemovedListeners(GetStorage<T>()).Add(listener);
        }

        private List<ComponentRemoved> GetRemovedListeners(IComponentStorage storage)
        {
            if (componentRemovedListeners.TryGetValue(storage, out List<ComponentRemoved> listeners))
            {
                return listeners;
            }
            else
            {
                List<ComponentRemoved> newListeners = new List<ComponentRemoved>();
                componentRemovedListeners.Add(storage, newListeners);
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

        public IComponentStorage GetStorage(Type type)
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
            // Run systems
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].Update(deltaTime);
            }
        }

        public void Render(float deltaTime)
        {
            // Run systems
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].Render(deltaTime);
            }
        }

        public void AddSystem(EntitySystem system)
        {
            system.World = this;
            systems.Add(system);
            system.OnAdded();

            SortSystems();
        }

        public void SortSystems()
        {
            // Sort systems by priority
            systems.Sort(delegate (EntitySystem system1, EntitySystem system2) { return system2.Priority.CompareTo(system1.Priority); });
        }

        public void RemoveSystem(EntitySystem system)
        {
            systems.Remove(system);
        }

        public EntityIterator Iterate(EntityFilter filter)
        {
            return new EntityIterator(allocator.GetLast(), this, filter);
        }
    }
}
