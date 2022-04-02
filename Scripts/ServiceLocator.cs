using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        
        public static void Register<T>(T service)
        {
               _services.Add(typeof(T), service); 
        }

        public static T Get<T>()
        {
            if (_services.ContainsKey(typeof(T)))
            {
                return (T) _services[typeof(T)];
            }
            else
            {
                throw new Exception($"The service of type {typeof(T).Name} hasn't been registered");
            }
        }

        public static T GetSafe<T>() where T : new()
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                Register(new T());
            }
            return Get<T>();
        }
}