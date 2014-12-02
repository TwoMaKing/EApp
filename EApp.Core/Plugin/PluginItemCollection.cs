using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.List;

namespace EApp.Core.Plugin
{
    public class PluginItemCollection<TPluginItem> : EntityArrayList<TPluginItem> where TPluginItem : PluginItem<TPluginItem>, new()
    {

    }
}
