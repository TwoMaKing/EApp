using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Common.Util;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using EApp.Plugin.Generic.RibbonStyle;
using EApp.UI.Controls.UIHandler;
using Xpress.Core.Common;
using Xpress.Core.Plugin;

namespace Xpress.UI
{
    public class XpressModulePluginHost : RibbonModulePluginHost 
    {
        public XpressModulePluginHost(IPluginProvider pluginProvider, CoreNavigationForm serviceProvider)
            : base(pluginProvider, serviceProvider)
        { 
            
        }
    }

}
