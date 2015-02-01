using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Plugin
{
    /// <summary>
    /// Host of Plugins. Plugin modules are loaded and run based on the specified Host. The host can be main form.
    /// </summary>
    public interface IHost
    {
        IPluginProvider PluginProvider { get; }
        
        IPluginServiceProviderFactory ServiceProvider { get; }

        /// <summary>
        /// Run the host.
        /// </summary>
        void Run();

        /// <summary>
        /// Exit the host.
        /// </summary>
        void Exit();
    }
}
