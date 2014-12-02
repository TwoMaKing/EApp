using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using Xpress.Core.Common;
using Xpress.Core.Entities;
using Xpress.Core.Logic;

namespace Xpress.UI.Plugins.Price
{
    public class AddNewDirectPricingAction : NonUIPluginAction
    {
        protected override object RunCore(IPluginServiceProvider serviceProvider)
        {
            return null;
        }

        protected override void UnloadCore()
        {
            return;
        }
    }
}
