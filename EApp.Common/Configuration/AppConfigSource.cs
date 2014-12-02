using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EApp.Common.Configuration
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
            string configSection = defaultConfigSectionName;
            try
            {
                object[] eAppConfigAttributes = typeof(EAppConfigurationSection).GetCustomAttributes(false);

                if (eAppConfigAttributes.Any(p => p.GetType().Equals(typeof(XmlRootAttribute))))
                {
                    XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)
                        eAppConfigAttributes.SingleOrDefault(p => p.GetType().Equals(typeof(XmlRootAttribute)));

                    if (!string.IsNullOrEmpty(xmlRootAttribute.ElementName) &&
                        !string.IsNullOrWhiteSpace(xmlRootAttribute.ElementName))
                    {
                        configSection = xmlRootAttribute.ElementName;
                    }
                }
            }
            catch // If any exception occurs, suppress the exception to uuse the default config section.
            {
                
            }

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
