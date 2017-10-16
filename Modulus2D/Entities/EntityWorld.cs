using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modulus2D.Graphics;

namespace Modulus2D.Entities
{
    public delegate void EntityCreated(Entity entity);
    public delegate void EntityRemoved(Entity entity);

    public class EntityWorld
    {
        private EntityAllocator allocator = new EntityAllocator();
        private List<EntitySystem> systems = new List<EntitySystem>();
        private Dictionary<Type, IComponentStorage> storages = new Dictionary<Type, IComponentStorage>();
        private Dictionary<Type, List<EntityCreated>> entityCreatedListeners = new Dictionary<Type, List<EntityCreated>>();
        private Dictionary<IComponentStorage, List<EntityRemoved>> entityRemovedListeners = new Dictionary<IComponentStorage, List<EntityRemoved>>();

        public Entity Add()
        {
            return new Entity(allocator.Create(), this);
        }

        public Entity Create(Builder builder)
        {
            Entity entity = Add();
            builder.Build(entity);
            builder.BuildGraphics(entity);

            return entity;
        }

        public void Remove(int id)
        {
            allocator.Remove(id);
            foreach (IComponentStorage storage in storages.Values)
            {
                // Notify listeners
                if (storage.Has(id))
                {
                    List<EntityRemoved> listeners = GetRemovedListeners(storage);
                    if (listeners.Count > 0)
                    {
                        Entity entity = new Entity(id, this);
                        foreach (EntityRemoved listener in listeners)
                        {
                            listener(entity);
                        }
                    }
                }

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

            List<EntityCreated> listeners = GetCreatedListeners<T>();
            if(listeners.Count > 0)
            {
                Entity entity = new Entity(id, this);
                foreach (EntityCreated listener in listeners)
                {
                    listener(entity);
                }
            }
        }

        public void AddCreatedListener<T>(EntityCreated listener) where T : IComponent
        {
            GetCreatedListeners<T>().Add(listener);
        }

        private List<EntityCreated> GetCreatedListeners<T>() where T : IComponent
        {
            if (entityCreatedListeners.TryGetValue(typeof(T), out List<EntityCreated> listeners))
            {
                return listeners;
            }
            else
            {
                List<EntityCreated> newListeners = new List<EntityCreated>();
                entityCreatedListeners.Add(typeof(T), newListeners);
                return newListeners;
            }
        }

        public void AddRemovedListener<T>(EntityRemoved listener) where T : IComponent
        {
            GetRemovedListeners(GetStorage<T>()).Add(listener);
        }

        private List<EntityRemoved> GetRemovedListeners(IComponentStorage storage)
        {
            if (entityRemovedListeners.TryGetValue(storage, out List<EntityRemoved> listeners))
            {
                return listeners;
            }
            else
            {
                List<EntityRemoved> newListeners = new List<EntityRemoved>();
                entityRemovedListeners.Add(storage, newListeners);
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
            // Run systems
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].Update(deltaTime);
            }
        }

        public void AddSystem(EntitySystem system)
        {
            system.World = this;
            systems.Add(system);
            system.AddedToWorld();

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
