using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.UI.Plugin
{
    /// <summary>
    /// This is a core interface for Express modules based on the plugin. 
    /// The each modules for Express needs to implement this interface.
    /// The module that has implemented this interface can be populated into the Ribbon menu and be executed.
    /// if you want to add a module or a funtion for the module you need to implement this interface 
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Occurs after running the module or funtion.
        /// </summary>
        event EventHandler<PluginLoadedEventArgs> Loaded;

        /// <summary>
        /// Occurs after unloading the module or function.
        /// </summary>
        event EventHandler Unloaded;

        /// <summary>
        /// Keep alive or only once
        /// </summary>
        LifetimeMode Lifetime { get; } 

        /// <summary>
        /// Execute module or funtion
        /// </summary>
        /// <param name="serviceProvider">Defines a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</param>
        void Run(IPluginServiceProvider serviceProvider);

        /// <summary>
        /// Unload this module or function.
        /// </summary>
        void Unload();
    }
}
