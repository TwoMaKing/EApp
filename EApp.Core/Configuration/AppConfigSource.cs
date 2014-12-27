using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EApp.Core.Configuration
{
    public class EAppConfigSource : IConfigSource
    {
        private EAppConfigurationSection configurationSection;

        private string defaultConfigSectionName = "EApp";

        public EAppConfigSource() 
        {
            LoadConfig();
        }

        private void LoadConfig() 
        {
            this.configurationSection = (EAppConfigurationSection)ConfigurationManager.GetSection(defaultConfigSectionName);
        }

        public EAppConfigurationSection Config
        {
            get 
            {
                return this.configurationSection;
            }
        }
    }
}
