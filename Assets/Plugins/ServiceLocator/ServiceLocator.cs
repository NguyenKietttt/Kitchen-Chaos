using System;
using System.Collections.Generic;

namespace UnityServiceLocator
{
    public sealed class ServiceLocator : IServiceLocator
    {
        public static IServiceLocator Instance => _instance.Value;
        private static readonly Lazy<IServiceLocator> _instance = new(() => { return new ServiceLocator(); });
        private ServiceLocator() { }

        private readonly Dictionary<Type, object> _serviceDic = new();

        public void Register<TService>(TService serviceInstance, Action onRegistered = null)
        {
            Type serviceType = typeof(TService);

            if (_serviceDic.ContainsKey(serviceType))
            {
                throw new Exception($"{serviceType.Name} has been already registered!");
            }

            _serviceDic.Add(serviceType, serviceInstance);
            onRegistered?.Invoke();
        }

        public TService Get<TService>()
        {
            Type serviceType = typeof(TService);

            if (_serviceDic.ContainsKey(serviceType))
            {
                return (TService)_serviceDic[serviceType];
            }

            throw new Exception($"{serviceType.Name} hasn't been registered!");
        }
    }
}
