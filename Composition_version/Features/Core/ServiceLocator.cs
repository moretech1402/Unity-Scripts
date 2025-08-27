using System;
using System.Collections.Generic;

namespace Core
{
    public static class ServiceLocator
    {
        static Dictionary<Type, object> _services = new();

        public static void Register<T>(T service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services[type] = service;
            }
            else
            {
                _services.Add(type, service);
            }
        }

        public static T Get<T>()
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }
            throw new KeyNotFoundException($"Service of type {type.Name} not registered.");
        }
        
        public static void Unregister<T>()
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
            else
            {
                throw new KeyNotFoundException($"Service of type {type.Name} not registered.");
            }
        }
    }
}
