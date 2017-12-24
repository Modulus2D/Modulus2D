using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Resources
{
    public class ResourceManager
    {
        private Dictionary<string, IResource> resources;

        public ResourceManager()
        {
            resources = new Dictionary<string, IResource>();
        }

        /// <summary>
        /// Loads or retrieves a resource of type T with the given path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T Get<T>(string path) where T : IResource
        {
            if(resources.TryGetValue(path, out IResource resource))
            {
                return (T)resource;
            } else
            {
                // Load resource
                T newResource = Activator.CreateInstance<T>();
                newResource.Load(path);

                resources.Add(path, newResource);

                return newResource;
            }
        }

        /// <summary>
        /// Unloads a resource with the given path if it has been loaded
        /// </summary>
        /// <param name="path"></param>
        public void Unload(string path)
        {
            if(resources.ContainsKey(path))
            {
                resources.Remove(path);
            }
        }
    }
}
