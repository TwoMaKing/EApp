using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EApp.Core.Application;
using Xpress.Chat.DataObjects;
using Xpress.Life.ModelBinder;

namespace Xpress.Life
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            ControllerBuilder.Current.SetControllerFactory(new EAppControllerFactory());

            ModelBinders.Binders.Add(typeof(PostDataObject), new PostModelBinder());

            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());

            EAppRuntime.Instance.Create();

 			//Enable  RESTful service in MVC
            //RouteTable.Routes.Add(new ServiceRoute("AppService", new WebServiceHostFactory(), typeof(AppService)));
        }
    }
}