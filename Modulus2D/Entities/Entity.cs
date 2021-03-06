﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    public class Entity
    {
        public int id;
        private EntityWorld world;

        public Entity(int entityId, EntityWorld entityWorld)
        {
            id = entityId;
            world = entityWorld;
        }

        public T AddComponent<T>(T component) where T : IComponent
        {
            world.AddComponent(id, component);

            return component;
        }

        public T GetComponent<T>() where T : IComponent
        {
            return world.GetComponent<T>(id);
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return world.HasComponent<T>(id);
        }

        public void RemoveComponent<T>() where T : IComponent
        {
            world.RemoveComponent<T>(id);
        }

        public void Destroy()
        {
            world.Remove(id);
        }
    }
}
