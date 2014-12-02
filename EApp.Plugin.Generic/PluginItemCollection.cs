using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.List;

namespace EApp.UI.Plugin
{
    public class PluginItemCollection<TPluginItem> : EntityArrayList<TPluginItem> where TPluginItem : PluginItem<TPluginItem>
    {

    }
}
