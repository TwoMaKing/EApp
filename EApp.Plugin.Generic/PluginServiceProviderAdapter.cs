using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Plugin;

namespace EApp.Plugin.Generic
{
    public class PluginServiceProviderAdapter : IPluginServiceProvider
    {
        private IDictionary<Type, object> serviceInstances;

        public PluginServiceProviderAdapter() 
        { 
            this.serviceInstances = new Dictionary<Type, object>();
        }

        public PluginServiceProviderAdapter(IDictionary<Type, object> services) 
        {
            this.serviceInstances = services;
        }

        public object this[Type serviceType]
        {
            get
            {
                return this.serviceInstances[serviceType];
            }
            set
            {
                this.serviceInstances[serviceType] = value;
            }
        }

        public bool Contains(Type serviceType)
        {
            return serviceInstances.ContainsKey(serviceType);
        }

        public void AddService(Type serviceType, object value)
        {
            if (this.serviceInstances.ContainsKey(serviceType))
            {
                this.RemoveService(serviceType);
            }

            this.serviceInstances.Add(serviceType, value);
        }

        public void AddServices(IDictionary<Type, object> services)
        {
            foreach (Type serviceTypeKey in services.Keys)
            {
                AddService(serviceTypeKey, services[serviceTypeKey]);
            }
        }

        public void RemoveService(Type serviceType)
        {
            this.serviceInstances.Remove(serviceType);
        }

        public object GetService(Type serviceType)
        {
            if (this.serviceInstances.ContainsKey(serviceType))
            {
                return this.serviceInstances[serviceType];
            }

            return null;
        }
    }
}
