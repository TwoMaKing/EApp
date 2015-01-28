using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace EApp.Core
{
    public interface IServiceLocator : IServiceProvider
    {
        object GetService(Type serviceType, ResolverOverride[] overrides);

        object GetService(Type serviceType, string name, ResolverOverride[] overrides);

        TService GetService<TService>();

        TService GetService<TService>(ResolverOverride[] overrides);

        TService GetService<TService>(string name);

        TService GetService<TService>(string name, ResolverOverride[] overrides);

        IEnumerable<TService> ResolveAll<TService>();

    }
}
