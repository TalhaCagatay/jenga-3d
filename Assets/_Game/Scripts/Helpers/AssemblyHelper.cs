using System;
using System.Collections.Generic;
using System.Linq;

namespace _Game.Scripts.Helpers
{
    public static class AssemblyHelper
    {
        /// <summary>
        /// Creates Instances Of Every Type Which Inherits From T Interface
        /// </summary>
        /// <typeparam name="T">Interface Type To Create</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> CreateInstancesOfType<T>()
        {
            var interfaceType = typeof(T);
            var result = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance);

            return result.Cast<T>();
        }
    }
}