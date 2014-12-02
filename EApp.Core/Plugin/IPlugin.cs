using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Plugin
{
    /// <summary>
    /// This is a core interface based on the plugin framework. 
    /// The each plugin module including user control mode UI screen, popup overlay form and non-UI action needs to implement this interface.
    /// The plugin module that has implemented this interface can be populated into the a core nav form (i.e. host main form) and be executed.
    /// If you want to add a module (e.g. user control mode UI screen, popup overlay form or a non-UI action funtion) for the host main form 
    /// you need to implement this interface 
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
        /// The Lifetime of the plugin. Keep alive or only once.
        /// Generally, UI screen is Keep alive, the non-UI function is only once. e.g. add, copy, delete, copy etc.
        /// </summary>
        LifetimeMode Lifetime { get; } 

        /// <summary>
        /// Execute this plugin module or funtion
        /// </summary>
        /// <param name="serviceProvider">Defines a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</param>
        void Run(IPluginServiceProvider serviceProvider);

        /// <summary>
        /// Unload this plugin module or function.
        /// </summary>
        void Unload();
    }
}
