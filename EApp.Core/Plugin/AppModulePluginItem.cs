using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Plugin
{
    /// <summary>
    /// Generic module/sub module item description in application  
    /// </summary>
    public class AppModulePluginItem : PluginItem<AppModulePluginItem>
    {
        public AppModulePluginItem(string name, NavigationNodeItem navigationNode)
            : this(name, string.Empty, navigationNode)
        {

        }

        public AppModulePluginItem(string name, string text, NavigationNodeItem navigationNode) :
            this(name, text, string.Empty, navigationNode)
        {

        }

        public AppModulePluginItem(string name, string text, string tooltip, NavigationNodeItem navigationNode) :
            this(name, text, tooltip, null, navigationNode)
        {

        }

        public AppModulePluginItem(string name, string text, string tooltip, Type providerType, NavigationNodeItem navigationNode) :
            base(name, text, tooltip, providerType, navigationNode)
        {

        }

        public string IconResourceName { get; set; }

        public bool Visible { get; set; }

        public bool Enabled { get; set; }

        public bool Checked { get; set; }

    }
}
