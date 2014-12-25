using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.WindowsMvc;
using EApp.Core.Exceptions;

namespace EApp.Windows.Mvc
{
    public class DefaultControllerFactory : IControllerFactory
    {
        public virtual IController CreateController(string controllerName)
        {
            if (!EAppRuntime.Instance.CurrentApp.ObjectContainer.Registered<IController>(controllerName))
            {
                throw new InfrastructureException("");
            }

            return EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve<IController>(controllerName);
        }

        public virtual IController CreateController(Type controllerType)
        {
            if (!EAppRuntime.Instance.CurrentApp.ObjectContainer.Registered(controllerType))
            {
                EAppRuntime.Instance.CurrentApp.ObjectContainer.RegisterType(controllerType);
            }

            return (IController)EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve(controllerType);
        }

        public Type GetControllerType(string controllerName)
        {
            if (EAppRuntime.Instance.CurrentApp.ConfigSource != null &&
                EAppRuntime.Instance.CurrentApp.ConfigSource.Config != null &&
                EAppRuntime.Instance.CurrentApp.ConfigSource.Config.WindowsMvc != null &&
                EAppRuntime.Instance.CurrentApp.ConfigSource.Config.WindowsMvc.Controllers != null)
            {
                string controllerTypeName = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.WindowsMvc.Controllers[controllerName].Type;

                return Type.GetType(controllerTypeName);
            }

            return null;
        }
    }
}
