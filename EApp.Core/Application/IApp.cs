using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Configuration;
using EApp.Core.Plugin;

namespace EApp.Core.Application
{
    /// <summary>
    /// Major components used in the EApp application including plugin framework, IOC container, configuration etc.
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// Gets the ConfigSource instance that was used
        /// for configuring the application.
        /// </summary>
        IConfigSource ConfigSource { get; }
        /// <summary>
        /// Gets the IObjectContainer instance with which the application
        /// registers or resolves the object dependencies.
        /// </summary>
        IObjectContainer ObjectContainer { get; }

        /// <summary>
        /// Get Plugin Host instance which is used to load modules and sub modules (plugin mode) in applications.
        /// </summary>
        IHost PluginHost { get; }

        /// <summary>
        /// Get windows mvc default controller factory builder.
        /// </summary>
        WindowsMvcControllerBuilder WinMvcControllerBuilder { get; }

        /// <summary>
        /// Get the list of resource manager instance. Resource manager is used for storing text, image, icon, stream etc.
        /// </summary>
        IDictionary<string, IResourceManager> ResourceManagers { get; }

        /// <summary>
        /// Starts the application.
        /// </summary>
        void Start();

        /// <summary>
        /// The event that occurs when the application is initializing.
        /// </summary>
        event EventHandler<AppInitEventArgs> AppInitialized;

    }
}
