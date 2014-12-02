using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Plugin
{
    public interface IPluginProvider 
    {
        IDictionary<NavigationNodeItem, IEnumerable<TPluginItem>> GetPlugins<TPluginItem>() where TPluginItem : PluginItem<TPluginItem>, new();
    }

}
