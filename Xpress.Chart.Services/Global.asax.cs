using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Routing;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using EApp.Core;
using EApp.Core.Application;

namespace Xpress.Chat.Services
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            this.RegisterRoutes();

            EAppRuntime.Instance.Create();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void RegisterRoutes()  
        {
            RouteTable.Routes.Add(new ServiceRoute("QueryService",   
                                  new WebServiceHostFactory(), typeof(QueryService)));  
        }  

    }
}