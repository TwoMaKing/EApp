using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using EApp.Plugin.Generic.RibbonStyle;
using EApp.UI.Controls.UIHandler;
using Xpress.Core.Common;
using Xpress.Core.Plugin;
using Xpress.Resources;

namespace Xpress.UI
{
    public partial class XpressAppNav : CoreNavigationForm
    {
        public XpressAppNav()
        {
            InitializeComponent();
        }

        public XpressAppNav(IPluginProvider pluginProvider) : base(pluginProvider, new CommonResourceManager())
        {
            InitializeComponent();
        }

        public override IPluginServiceProvider ServiceProvider
        {
            get
            {
                IPluginServiceProvider baseServiceProvider = base.ServiceProvider;

                baseServiceProvider.AddService(typeof(IContainerControl), this.mdiUIContainer);

                return baseServiceProvider;
            }
        }

    }
}
