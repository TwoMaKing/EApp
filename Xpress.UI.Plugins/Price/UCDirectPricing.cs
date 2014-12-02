using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Common.Util;
using EApp.Core.Application;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using EApp.Plugin.Generic.RibbonStyle;
using EApp.UI.Controls.GridView;
using EApp.UI.Controls.UIHandler;
using Xpress.Core.Common;
using Xpress.Core.Entities;
using Xpress.Core.Logic;
using Xpress.Core.Plugin;

namespace Xpress.UI.Plugins.Price
{
    public partial class UCDirectPricing : UCRibbonPluginViewBase
    {
        public UCDirectPricing()
        {
            InitializeComponent();
        }

    }
}
