using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.UI.Plugin
{
    public interface IPluginProvider<TPluginItem> where TPluginItem : PluginItem<TPluginItem>
    {
        IDictionary<NavigationNodeItem, IEnumerable<TPluginItem>> GetPlugins();
    }
}
