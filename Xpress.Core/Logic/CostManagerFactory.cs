using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using EApp.Common.Mapper;
using EApp.Core;
using EApp.Core.Application;
using Xpress.Core.Common;
using Xpress.Core.Entities;

namespace Xpress.Core.Logic
{
    public class CostManagerFactory
    {
        private readonly static CostManagerFactory costManagerFactory = new CostManagerFactory();

        private static Type specifiedCostManagerType;

        public CostManagerFactory() { }

        public static BaseCostManager<TCostLineItem> GetCostManager<TCostLineItem>() where TCostLineItem : CostLineItemBase
        {
            string costManagerTypeName = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.MiscSettings[typeof(TCostLineItem).Name].value;

            specifiedCostManagerType = Type.GetType(costManagerTypeName);

            if (!EAppRuntime.Instance.CurrentApp.ObjectContainer.Registered(specifiedCostManagerType))
            {
                EAppRuntime.Instance.CurrentApp.ObjectContainer.RegisterType(specifiedCostManagerType, ObjectLifetimeMode.Singleton);
            }

            return (BaseCostManager<TCostLineItem>)EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve(specifiedCostManagerType);
        }

        public static CostManagerFactory GetInstance(CostLineType costType)
        {
            string costManagerTypeName = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.MiscSettings[costType.ToString()].value;

            specifiedCostManagerType = Type.GetType(costManagerTypeName);

            if (!EAppRuntime.Instance.CurrentApp.ObjectContainer.Registered(specifiedCostManagerType))
            {
                EAppRuntime.Instance.CurrentApp.ObjectContainer.RegisterType(specifiedCostManagerType, ObjectLifetimeMode.Singleton);
            }

            return costManagerFactory;
        }

        public Type CurrentCostManagerType
        {
            get { return specifiedCostManagerType; }
        }
    }
}
