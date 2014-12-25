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
using Castle.DynamicProxy;

namespace Xpress.UI.Plugins.Cost
{
    public class CostAddNewAction : NonUIPluginAction
    {
        protected override object RunCore(IPluginServiceProvider serviceProvider)
        {
            CostLineType currentCostType = (CostLineType)serviceProvider.GetService(typeof(CostLineType));

            CostManagerFactory costManagerFactory = CostManagerFactory.GetInstance(currentCostType);

            object currentCostManagerObject = EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve(costManagerFactory.CurrentCostManagerType);

            ProxyGenerator proxyGenerator = new ProxyGenerator();

            var options = new ProxyGenerationOptions();

            options.AddMixinInstance(currentCostManagerObject);

            ICostManager costManager =
                (ICostManager)proxyGenerator.CreateClassProxy<CostManagerFactory>(options);

            costManager.AddCostLine();

            return costManager.CurrentDisplayedCostLines;
        }

        protected override void UnloadCore()
        {
            return;
        }
    }
}
