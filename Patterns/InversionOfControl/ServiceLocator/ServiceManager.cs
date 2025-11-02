using System;
using System.Collections.Generic;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace SombraStudios.Shared.Patterns.InversionOfControl.ServiceLocator
{
    public class ServiceManager
    {
        private readonly Dictionary<Type, object> _services = new();
        public IEnumerable<object> RegisteredServices => _services.Values;

        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out var foundService))
            {
                service = foundService as T;
                return true;
            }

            service = null;
            return false;
        }

        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out object obj))
            {
                return obj as T;
            }

            throw new ArgumentException($"Service of type {typeof(T).FullName} is not registered.");
        }

        public ServiceManager Register<T>(T service)
        {
            Type type = typeof(T);
            if (!_services.TryAdd(type, service))
            {
                Debug.LogError($"Service of type {type.FullName} is already registered.");
            }

            return this;
        }

        public ServiceManager Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
            {
                throw new ArgumentException($"Service is not of type {type.FullName}.");
            }

            if (!_services.TryAdd(type, service))
            {
                Debug.LogError($"Service of type {type.FullName} is already registered.");
            }

            return this;
        }
    }
}
