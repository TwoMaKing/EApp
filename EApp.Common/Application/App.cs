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
    public class App : IApp
    {
        private IConfigSource configSource;
        private IObjectContainer objectContainer;

        public event EventHandler<AppInitEventArgs> AppInitialized;

        public App(IConfigSource configSource) 
        {
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

            this.configSource = configSource;

            string objectContainerProviderName = configSource.Config.ObjectContainer.Provider;

            if (string.IsNullOrEmpty(objectContainerProviderName) ||
                string.IsNullOrWhiteSpace(objectContainerProviderName))
            {
                throw new ConfigException("The ObjectContainer provider has not been defined in the ConfigSource.");
            }

            string objectContainerAssemblyTypeName = this.configSource.Config.ObjectContainer.Provider;

            if (string.IsNullOrEmpty(objectContainerAssemblyTypeName) ||
                string.IsNullOrWhiteSpace(objectContainerAssemblyTypeName))
            {
                throw new ConfigException("The ObjectContainer Type Name has not been defined in the ConfigSource.");
            }

            string[] objectContainerAssemblyTypeNameArray = objectContainerAssemblyTypeName.Split(
                new string[]{","},  StringSplitOptions.RemoveEmptyEntries);

            if (objectContainerAssemblyTypeNameArray.Length < 2)
            {
                throw new ConfigException("The ObjectContainer Type Name error in the ConfigSource.");
            }

            string objectContainerTypeName = objectContainerAssemblyTypeNameArray[0];
            string objectContainerAssemblyName = objectContainerAssemblyTypeNameArray[1];

            Assembly objectContainerAssembly = Assembly.Load(new AssemblyName(objectContainerAssemblyName));
            
            Type objectContainerType = objectContainerAssembly.GetType(objectContainerTypeName);

            if (objectContainerType == null)
            {
                throw new InfrastructureException("The ObjectContainer defined by type {0} doesn't exist.", objectContainerAssemblyTypeName);
            }

            IObjectContainerFactory currentObjectContainerFactory = (IObjectContainerFactory)Activator.CreateInstance(objectContainerType);

            if (currentObjectContainerFactory != null)
            {
                this.objectContainer = currentObjectContainerFactory.ObjectContainer;
            }

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

        protected virtual void OnAppInitialized() 
        { 
            if (this.AppInitialized != null)
            { 
                this.AppInitialized(this,new AppInitEventArgs(this.configSource, this.objectContainer));
            }
        }

        public virtual void StartCore() 
        { 
            
        }

        public void Start()
        {
            this.StartCore();
            this.OnAppInitialized();
        }

    }
}
