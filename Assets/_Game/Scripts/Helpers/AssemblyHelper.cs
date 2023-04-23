using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Game.Scripts.Helpers
{
    public static class AssemblyHelper
    {
        /// <summary>
        /// Creates Instances Of Every Type Which Inherits From T Interface
        /// </summary>
        /// <typeparam name="T">Interface Type To Create</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> CreateInstancesOfNonMonoTypes<T>()
        {
            var interfaceType = typeof(T);
            var result = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract && !x.IsSubclassOf(typeof(MonoBehaviour)))
                .Select(Activator.CreateInstance);

            return result.Cast<T>();
        }

        public static IEnumerable<T> GetInstancesOfMonoTypes<T>(this Transform transform)
        {
            var interfaces = new List<T>(); ;
            foreach(Transform rootGameObject in transform)
            {
                var childrenInterfaces = rootGameObject.GetComponentsInChildren<T>(true);
                foreach( var childInterface in childrenInterfaces )
                    interfaces.Add(childInterface);
            }
            return interfaces;
        }
    }
}