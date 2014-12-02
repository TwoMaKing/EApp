using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Configuration;
using EApp.Common.IOC;

namespace EApp.Common.Application
{
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
        /// Starts the application.
        /// </summary>
        void Start();

        /// <summary>
        /// The event that occurs when the application is initializing.
        /// </summary>
        event EventHandler<AppInitEventArgs> AppInitialized;

    }
}
