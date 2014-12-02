using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EApp.Core.Plugin;

namespace EApp.UI.Plugin
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPluginController<TPluginItem> where TPluginItem : PluginItem<TPluginItem>
    {
        /// <summary>
        /// Occurs when the controller is loading.
        /// </summary>
        event CancelEventHandler Loading;

        /// <summary>
        /// Occurs after the controller is executed.
        /// </summary>
        event EventHandler<PluginLoadedEventArgs> Loaded;

        /// <summary>
        /// Occurs when the controller is unloading.
        /// </summary>
        event CancelEventHandler Unloading;

        /// <summary>
        /// Occurs when the controller is unloaded.
        /// </summary>
        event EventHandler Unloaded;

        /// <summary>
        /// Defines a mechanism for retrieving a service object; that is, an object that
        //  provides custom support to other objects
        /// </summary>
        IPluginServiceProvider ServiceProvider { get; }

        /// <summary>
        /// PluginItem
        /// </summary>
        TPluginItem PluginItem { get; }

        /// <summary>
        /// IPlugin
        /// </summary>
        IPlugin PluginInstance { get; }

        /// <summary>
        /// Whether the current controller/module/function is running
        /// </summary>
        bool Running { get; }

        /// <summary>
        /// Execute the current represented module or funtion
        /// </summary>
        void Load();

        /// <summary>
        /// Unload the current represented module or funtion
        /// </summary>
        void Unload(bool disposing);
    }
}
