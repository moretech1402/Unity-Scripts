using System;
using System.Collections.Generic;

namespace MC.Core
{
    public static class ServiceLocator
    {
        static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T service) where T : class
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

        public static bool TryGet<T>(out T service) where T : class
        {
            if (_services.TryGetValue(typeof(T), out var obj))
            {
                service = (T)obj;
                return true;
            }

            service = null;
            return false;
        }

        public static T Get<T>() where T : class
        {
            if (TryGet<T>(out var service))
                return service;

            throw new KeyNotFoundException($"Service of type {typeof(T).Name} not registered.");
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

        /// <summary>
        /// Clears all registered services from the locator.
        /// Useful for unit testing to ensure isolation between tests.
        /// </summary>
        public static void Clear() => _services.Clear();
    }
}
