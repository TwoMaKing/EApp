using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Exceptions;
using EApp.Core.Plugin;

namespace EApp.Core.Configuration
{
    public class RegularConfigSource : IConfigSource
    {
        private EAppConfigurationSection appConfigurationSection;

        public RegularConfigSource() 
        {
            this.appConfigurationSection = new EAppConfigurationSection();
            this.appConfigurationSection.Application = new CurrentApplicationElement();
            this.appConfigurationSection.ObjectContainer = new CurrentObjectContainerElement();
            this.appConfigurationSection.Logger = new CurrentLoggerElement();
            this.appConfigurationSection.PluginContainer = new PluginContainerElement();
            this.appConfigurationSection.PluginContainer.Host = new PluginHostElement();
            this.appConfigurationSection.PluginContainer.Host.ServiceProvider = new SingleProviderElement();
            this.appConfigurationSection.PluginContainer.Host.PluginProvider = new SingleProviderElement();
            this.appConfigurationSection.PluginContainer.PluginRegisters = new PluginRegisterElementCollection();
            this.appConfigurationSection.MiscSettings = new MiscSettingElementCollection();
            this.appConfigurationSection.ResourceManagers = new ResourceElementCollection();
        }

        public Type Application 
        {
            get 
            {
                return Type.GetType(this.appConfigurationSection.Application.Provider);
            }
            set
            {
                this.appConfigurationSection.Application.Provider = value.AssemblyQualifiedName;
            }
        }

        public Type ObjectContainer 
        {
            get 
            {
                return Type.GetType(this.appConfigurationSection.ObjectContainer.Provider);
            }
            set
            {
                this.appConfigurationSection.ObjectContainer.Provider = value.AssemblyQualifiedName;
            }
        }

        public bool InitFromConfigFile 
        {
            get 
            {
                return this.appConfigurationSection.ObjectContainer.InitFromConfigFile;
            }
            set
            {
                this.appConfigurationSection.ObjectContainer.InitFromConfigFile = value;
            }
        }

        public string SectionName 
        {
            get 
            {
                return this.appConfigurationSection.ObjectContainer.SectionName;
            }
            set
            {
                this.appConfigurationSection.ObjectContainer.SectionName = value;
            }
        }


        public Type Logger 
        {
            get 
            {
                return Type.GetType(this.appConfigurationSection.Logger.Provider);
            }
            set
            {
                this.appConfigurationSection.Logger.Provider = value.AssemblyQualifiedName;
            }
        }


        public Type PluginHost 
        {
            get 
            {
                return Type.GetType(this.appConfigurationSection.PluginContainer.Host.Provider);
            }
            set
            {
                this.appConfigurationSection.PluginContainer.Host.Provider = value.AssemblyQualifiedName;
            }
        }

        public Type PluginServiceProvider 
        {
            get 
            {
                return Type.GetType(this.appConfigurationSection.PluginContainer.Host.ServiceProvider.Provider);
            }
            set
            {
                this.appConfigurationSection.PluginContainer.Host.ServiceProvider.Provider = value.AssemblyQualifiedName;
            }
        }

        public Type PluginProvider 
        {
            get 
            {
                return Type.GetType(this.appConfigurationSection.PluginContainer.Host.PluginProvider.Provider);
            }
            set
            {
                this.appConfigurationSection.PluginContainer.Host.PluginProvider.Provider = value.AssemblyQualifiedName;
            }
        }

        public void AddAppPlugin(Type type, string name) 
        {
            if (!typeof(IPlugin).IsAssignableFrom(type))
            {
                throw new ConfigException("Type '{0}' is not a plugin.", type);
            }

            if (this.appConfigurationSection.PluginContainer == null)
            {
                this.appConfigurationSection.PluginContainer = new PluginContainerElement();
            }

            if (this.appConfigurationSection.PluginContainer.PluginRegisters == null)
            {
                this.appConfigurationSection.PluginContainer.PluginRegisters = new PluginRegisterElementCollection();
            }

            foreach (PluginRegisterElement appPluginItem in this.appConfigurationSection.PluginContainer.PluginRegisters)
            {
                if (appPluginItem.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) ||
                    appPluginItem.Type.Equals(type.AssemblyQualifiedName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
            }

            PluginRegisterElement newModulePlugin = new PluginRegisterElement();
            newModulePlugin.Type = type.AssemblyQualifiedName;
            newModulePlugin.Name = name;

            this.appConfigurationSection.PluginContainer.PluginRegisters.Add(newModulePlugin);
        }

        public void AddMiscSetting(string key, string value) 
        {
            if (this.appConfigurationSection.MiscSettings == null)
            {
                this.appConfigurationSection.MiscSettings = new MiscSettingElementCollection();
            }

            foreach (MiscSettingAddElement exstingMiscSettingItem in this.appConfigurationSection.MiscSettings)
            {
                if (exstingMiscSettingItem.key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
            }

            MiscSettingAddElement newMiscSettingItem = new MiscSettingAddElement();
            newMiscSettingItem.key = key;
            newMiscSettingItem.value = value;

            this.appConfigurationSection.MiscSettings.Add(newMiscSettingItem);
        }

        public void AddResourceManager(Type type, string name) 
        {
            if (!typeof(IResourceManager).IsAssignableFrom(type))
            {
                throw new ConfigException("Type '{0}' is not a resource.", type);
            }

            if (this.appConfigurationSection.ResourceManagers == null)
            {
                this.appConfigurationSection.ResourceManagers = new ResourceElementCollection();
            }

            foreach (NameTypeElement resourceItem in this.appConfigurationSection.ResourceManagers)
            {
                if (resourceItem.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) ||
                    resourceItem.Type.Equals(type.AssemblyQualifiedName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
            }

            NameTypeElement newResourceItem = new NameTypeElement();
            newResourceItem.Type = type.AssemblyQualifiedName;
            newResourceItem.Name = name;

            this.appConfigurationSection.ResourceManagers.Add(newResourceItem);
        }

        public EAppConfigurationSection Config
        {
            get 
            {
                return this.appConfigurationSection;
            }
        }
    }
}
