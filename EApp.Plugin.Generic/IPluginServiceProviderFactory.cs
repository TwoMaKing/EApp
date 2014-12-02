using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.UI.Plugin
{
    public interface IPluginServiceProviderFactory
    {
        IPluginServiceProvider ServiceProvider { get; }
    }
}
