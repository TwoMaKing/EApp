using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EApp.Common.Configuration;
using EApp.Common.Exceptions;
using EApp.Common.IOC;

namespace EApp.Common.Application
{
    public sealed class EAppRuntime
    {
        private static readonly EAppRuntime appRuntime = new EAppRuntime();

        private IApp currrentApplication;

        private EAppRuntime() 
        { 
        
        }

        public static EAppRuntime Instance 
        {
            get 
            {
              
                return appRuntime;
            }
        }

        public IApp App 
        {
            get 
            {
                return this.currrentApplication;
            }
        }

        public IApp Create(IConfigSource configSource) 
        {
            if (configSource.Config == null ||
                configSource.Config.Application == null)
            {
                throw new ConfigException("Either EApp configuration or EApp application configuration has not been initialized in the ConfigSource instance.");
            }

            string typeName = configSource.Config.Application.Provider;

            if (string.IsNullOrEmpty(typeName))
            {
                throw new ConfigException("The provider type of the Application has not been defined in the ConfigSource yet.");
            }

            Type appType = Type.GetType(typeName);

            if (appType == null)
            {
                throw new InfrastructureException("The application provider defined by type '{0}' doesn't exist.", typeName);
            }

            IApp app = (IApp)Activator.CreateInstance(appType);

            this.currrentApplication = app;

            return this.currrentApplication;
        }

        public IApp Create() 
        {
            return this.Create(new EAppConfigSource());
        }
    }
}
