using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using EApp.Plugin.Generic.RibbonStyle;
using Xpress.Core.Common;

namespace Xpress.Core.Plugin
{
    public class XpressModulePluginItem : RibbonModulePluginItem
    {
        public XpressModulePluginItem() : this(string.Empty, null) { }

        public XpressModulePluginItem(string name, NavigationNodeItem navigationNode)
            : this(name, string.Empty, navigationNode)
        {

        }

        public XpressModulePluginItem(string name, string text, NavigationNodeItem navigationNode) :
            this(name, text, string.Empty, navigationNode)
        {

        }

        public XpressModulePluginItem(string name, string text, string tooltip, NavigationNodeItem navigationNode) :
            this(name, text, tooltip, null, navigationNode)
        {

        }

        public XpressModulePluginItem(string name, string text, string tooltip, Type providerType, NavigationNodeItem navigationNode) :
            base(name, text, tooltip, providerType, navigationNode)
        {

        }
    }
}
