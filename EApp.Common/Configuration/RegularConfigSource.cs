using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Configuration
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

            this.appConfigurationSection.AppPlugins = new AppPluginElementCollection();
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

        public void AddAppPlugin(Type type, string name) 
        {
            if (this.appConfigurationSection.AppPlugins == null)
            {
                this.appConfigurationSection.AppPlugins = new AppPluginElementCollection();
            }

            foreach (AppPluginElement appPluginItem in this.appConfigurationSection.AppPlugins) 
            {
                if (appPluginItem.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) ||
                    appPluginItem.Type.Equals(type.AssemblyQualifiedName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
            }

            AppPluginElement modulePlugin = new AppPluginElement();
            modulePlugin.Type = type.AssemblyQualifiedName;
            modulePlugin.Name = name;
            this.appConfigurationSection.AppPlugins.Add(modulePlugin);
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
