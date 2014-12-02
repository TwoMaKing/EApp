using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.List;


namespace EApp.Core.Plugin
{
    public interface IPluginManager<TPluginItem> where TPluginItem : PluginItem<TPluginItem>, new()
    {
        event EventHandler<CancelEventArgs<TPluginItem>> PluginAdding;

        event EventHandler<EventArgs<TPluginItem>> PluginAdded;

        event EventHandler<CancelEventArgs<TPluginItem>> PluginRemoving;

        event EventHandler<EventArgs<TPluginItem>> PluginRemoved;

        PluginControllerCollection<TPluginItem> PluginControllers { get; }

        void Initialize();

        void Refresh();

        void RegisterPlugin(TPluginItem item);

        void DisposePlugin(TPluginItem item);
    }

}
