using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Plugin
{
    public interface IPluginServiceProviderFactory
    {
        IPluginServiceProvider ServiceProvider { get; }
    }
}
