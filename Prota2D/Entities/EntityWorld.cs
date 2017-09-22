using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public class EntityWorld
    {
        public Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
        private List<IEntitySystem> systems = new List<IEntitySystem>();
        private ComponentRegister register = new ComponentRegister();
        private int currentId = 0;

        public Entity Add()
        {
            Entity entity = new Entity(currentId);
            currentId += 1;

            entities.Add(entity.id, entity);

            return entity;
        }

        public void Remove(Entity entity)
        {
            entities.Remove(entity.id);
        }

        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            return register.Get<T>()[entity.id];
        }   

        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {
            register.Set(entity.id, component);
        }

        public bool HasComponent<T>(Entity entity) where T : IComponent
        {
            return register.Contains<T>(entity.id);
        }

        public bool HasComponentType(Entity entity, Type type)
        {
            return register.ContainsType(entity.id, type);
        }

        public void Update()
        {
            foreach (IEntitySystem system in systems)
            {
                system.Update(this);
            }
        }

        public void AddSystem(IEntitySystem system)
        {
            systems.Add(system);
        }

        public EntityIterator Iterate(EntityFilter filter)
        {
            return new EntityIterator(entities.Values.ToList(), this, filter);
        }
    }
}
