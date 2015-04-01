using EApp.Core.Application;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Xpress.Chat.ServiceContracts;

namespace Xpress.Life
{
    public class EAppControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            return base.CreateController(requestContext, controllerName);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            IUnityContainer unityContainer = EAppRuntime.Instance.CurrentApp.ObjectContainer.GetWrapperContainer<IUnityContainer>();

            return (IController)unityContainer.Resolve(controllerType);   

            //return base.GetControllerInstance(requestContext, controllerType);
        }

        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            return base.GetControllerType(requestContext, controllerName);
        }
    }
}