using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.UI.Plugin
{
    public interface IPluginServiceProvider : IServiceProvider
    {
        object this[Type serviceType] { get; set; }

        void AddService(Type serviceType, object value);

        void AddServices(IDictionary<Type, object> services);

        void RemoveService(Type serviceType);
    }
}
