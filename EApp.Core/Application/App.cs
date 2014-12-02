using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EApp.Core;
using EApp.Core.Configuration;
using EApp.Core.Exceptions;
using EApp.Core.Plugin;
using EApp.Core.WindowsMvc;

namespace EApp.Core.Application
{
    public class App : IApp
    {
        private IConfigSource configSource;

        private IObjectContainer objectContainer;

        private IHost pluginHost;

        private WindowsMvcControllerBuilder winMvcControllerBuilder = new WindowsMvcControllerBuilder();

        private IDictionary<string, IResourceManager> resourceManagers = new Dictionary<string, IResourceManager>();

        public event EventHandler<AppInitEventArgs> AppInitialized;

        public App(IConfigSource configSource)
        {
            #region Check whether the required or mandatory EApp configuration has been configured in config file.

            if (configSource == null)
            {
                throw new ArgumentNullException("configSource");
            }

            if (configSource.Config == null)
            {
                throw new ConfigException("EAppConfigSource has not been defined in the ConfigSource instance.");
            }

            if (configSource.Config.ObjectContainer == null)
            {
                throw new ConfigException("No ObjectContainer instance has been specified in the EAppConfigSource.");
            }

            #endregion

            this.configSource = configSource;

            #region If default used object container (i.e. default used IOC component) has been configured in config file then create it by Activator.CreateInstance

            string objectContainerFactoryProviderName = configSource.Config.ObjectContainer.Provider;

            if (string.IsNullOrEmpty(objectContainerFactoryProviderName) ||
                string.IsNullOrWhiteSpace(objectContainerFactoryProviderName))
            {
                throw new ConfigException("The ObjectContainer provider has not been defined in the ConfigSource.");
            }

            Type objectContainerFactoryType = Type.GetType(objectContainerFactoryProviderName);

            if (objectContainerFactoryType == null)
            {
                throw new InfrastructureException("The ObjectContainer defined by type {0} doesn't exist.", objectContainerFactoryProviderName);
            }

            IObjectContainerFactory currentObjectContainerFactory = (IObjectContainerFactory)Activator.CreateInstance(objectContainerFactoryType);

            if (currentObjectContainerFactory != null)
            {
                this.objectContainer = currentObjectContainerFactory.ObjectContainer;
            }

            // if object container need to be initialized from config file. E.g. Unity from config file.
            if (this.configSource.Config.ObjectContainer.InitFromConfigFile)
            {
                string sectionName = this.configSource.Config.ObjectContainer.SectionName;

                if (!string.IsNullOrEmpty(sectionName) && !string.IsNullOrWhiteSpace(sectionName))
                {
                    this.objectContainer.InitializeFromConfigFile(sectionName);
                }
                else
                {
                    throw new ConfigException("Section name for the ObjectContainer configuration should also be provided when InitFromConfigFile has been set to true.");
                }
            }

            #endregion

            #region if resource managers have been configured in config file then create these resource managers

            if (configSource.Config.ResourceManagers != null &&
                configSource.Config.ResourceManagers.ElementInformation.IsPresent)
            {

                foreach (NameTypeElement resourceItem in configSource.Config.ResourceManagers)
                {
                    string resourceName = resourceItem.Name;

                    string resourceAssemblyName = resourceItem.Type;

                    if (!string.IsNullOrEmpty(resourceName) &&
                        !string.IsNullOrEmpty(resourceAssemblyName))
                    {
                        Type resourceAssemblyType = Type.GetType(resourceAssemblyName);

                        if (resourceAssemblyType == null)
                        {
                            continue;
                        }

                        this.ObjectContainer.RegisterType<IResourceManager>(resourceAssemblyType, resourceName);

                        this.resourceManagers.Add(resourceName, this.ObjectContainer.Resolve<IResourceManager>(resourceName));
                    }
                }
                
            }

            #endregion

            #region If module plugin architecture has been configured in config file then create plugin host, plugin provider and service provider

            if (configSource.Config.PluginContainer != null &&
                configSource.Config.PluginContainer.ElementInformation.IsPresent &&
                configSource.Config.PluginContainer.Host != null &&
                configSource.Config.PluginContainer.Host.ElementInformation.IsPresent)
            {
                if (configSource.Config.PluginContainer.Host.ServiceProvider == null)
                {
                    throw new ConfigException("Plugin Service Provider configuration has not been initialized in the ConfigSource instance.");
                }

                if (configSource.Config.PluginContainer.Host.PluginProvider == null)
                {
                    throw new ConfigException("Plugin Provider configuration has not been initialized in the ConfigSource instance.");
                }
                
                string hostTypeName = configSource.Config.PluginContainer.Host.Provider;

                if (string.IsNullOrEmpty(hostTypeName) ||
                    string.IsNullOrWhiteSpace(hostTypeName))
                {
                    throw new ConfigException("The Plugin Host Type Name has not been defined in the Plugin Config Source.");
                }

                string pluginProviderTypeName = configSource.Config.PluginContainer.Host.PluginProvider.Provider;

                if (string.IsNullOrEmpty(pluginProviderTypeName) ||
                    string.IsNullOrWhiteSpace(pluginProviderTypeName))
                {
                    throw new ConfigException("The Plugin Provider Type Name has not been defined in the Plugin Config Source.");
                }

                string serviceProviderFacotryTypeName = configSource.Config.PluginContainer.Host.ServiceProvider.Provider;

                if (string.IsNullOrEmpty(serviceProviderFacotryTypeName) ||
                    string.IsNullOrWhiteSpace(serviceProviderFacotryTypeName))
                {
                    throw new ConfigException("The Plugin Service Provider Type Name has not been defined in the Plugin Config Source.");
                }

                Type hostType = Type.GetType(hostTypeName);

                if (hostType == null)
                {
                    throw new InfrastructureException("The Plugin Host defined by type {0} doesn't exist.", hostTypeName);
                }

                Type pluginProviderType = Type.GetType(pluginProviderTypeName);

                if (pluginProviderType == null)
                {
                    throw new InfrastructureException("The Plugin Provider defined by type {0} doesn't exist.", pluginProviderTypeName);
                }

                Type serviceProviderFacotryType = Type.GetType(serviceProviderFacotryTypeName);

                if (serviceProviderFacotryType == null)
                {
                    throw new InfrastructureException("The Plugin Service Provider defined by type {0} doesn't exist.", serviceProviderFacotryTypeName);
                }

                IPluginProvider pluginProvider = (IPluginProvider)Activator.CreateInstance(pluginProviderType);

                IPluginServiceProviderFactory serviceProviderFactory = (IPluginServiceProviderFactory)Activator.CreateInstance(serviceProviderFacotryType, new object[] { pluginProvider });

                this.pluginHost = (IHost)Activator.CreateInstance(hostType, new object[] { pluginProvider, serviceProviderFactory });

                // If plugin types have been configured in config file then register them into the object container for IOC.
                // e.g. when clicking a button to load a module plugin UI get the plugin instance by name from object container.
                if (configSource.Config.PluginContainer != null &&
                    configSource.Config.PluginContainer.ElementInformation.IsPresent &&
                    configSource.Config.PluginContainer.PluginRegisters != null &&
                    configSource.Config.PluginContainer.PluginRegisters.Count > 0)
                {
                    foreach (PluginRegisterElement pluginRegisterItem in configSource.Config.PluginContainer.PluginRegisters) 
                    {
                        string pluginName = pluginRegisterItem.Name;

                        string pluginTypeName = pluginRegisterItem.Type;

                        if (!string.IsNullOrEmpty(pluginName) &&
                            !string.IsNullOrEmpty(pluginTypeName))
                        {
                            Type pluginAssemblyType = Type.GetType(pluginTypeName);

                            if (pluginAssemblyType == null)
                            {
                                continue;
                            }

                            this.ObjectContainer.RegisterType(typeof(IPlugin), pluginAssemblyType, pluginName);
                        }
                    }
                }
            }

            #endregion

            #region If windows mvc controller list have been configured in config file then register controller type

            if (configSource.Config.WindowsMvc != null &&
                configSource.Config.WindowsMvc.ElementInformation.IsPresent &&
                configSource.Config.WindowsMvc.Controllers != null)
            {
                foreach (NameTypeElement controllerRegisterItem in configSource.Config.WindowsMvc.Controllers) 
                {
                    string controllerName = controllerRegisterItem.Name;

                    string controllerTypeName = controllerRegisterItem.Type;

                    if (!string.IsNullOrEmpty(controllerName) &&
                        !string.IsNullOrEmpty(controllerTypeName))
                    {
                        Type controllerAssemblyType = Type.GetType(controllerTypeName);

                        if (controllerAssemblyType == null)
                        {
                            continue;
                        }

                        this.ObjectContainer.RegisterType(typeof(IController), controllerAssemblyType, controllerName);
                    }
                }
            }

            #endregion
        }
        
        public IConfigSource ConfigSource
        {
            get 
            {
                return this.configSource;
            }
        }

        public IObjectContainer ObjectContainer
        {
            get 
            { 
                return this.objectContainer; 
            }
        }

        public IHost PluginHost
        {
            get 
            {
                return this.pluginHost;
            }
        }

        public WindowsMvcControllerBuilder WinMvcControllerBuilder
        {
            get 
            {
                return this.winMvcControllerBuilder;
            }
        }

        public IDictionary<string, IResourceManager> ResourceManagers
        {
            get 
            { 
                return this.resourceManagers; 
            }
        }

        public void Start()
        {
            this.StartCore();
            this.OnAppInitialized();
        }

        protected virtual void StartCore()
        {
            return;
        }

        protected virtual void OnAppInitialized()
        {
            if (this.AppInitialized != null)
            {
                this.AppInitialized(this, new AppInitEventArgs(this.configSource, this.objectContainer));
            }
        }

    }
}
