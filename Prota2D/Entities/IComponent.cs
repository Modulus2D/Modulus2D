using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Entities
{
    public interface IComponent
    {
    }

    static class ComponentLists
    {
        static class PerType<T> where T : IComponent
        {
            public static List<T> list;
        }

        public static List<T> Get<T>() where T : IComponent
        {
            List<T> list = PerType<T>.list;

            if (list == null)
            {
                Register<T>();
                list = PerType<T>.list;
            }

            return list;
        }

        public static void Register<T>() where T : IComponent
        {
            PerType<T>.list = new List<T>();
        }

        public static List<T> GetByExample<T>(T ignoredExample) where T : IComponent
        {
            return Get<T>();
        }
    }
}
