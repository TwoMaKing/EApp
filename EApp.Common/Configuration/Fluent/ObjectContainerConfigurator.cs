using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Configuration.Fluent
{
    public class ObjectContainerConfigurator : TypeSpecifiedConfigSourceConfigurator
    {
        private bool initFromConfigFile;
        private string sectionName;

        public ObjectContainerConfigurator(IConfigurator<RegularConfigSource> context, Type type, bool initFromConfigFile, string sectionName)
            : base(context, type)
        {
            this.initFromConfigFile = initFromConfigFile;
            this.sectionName = sectionName;
        }

        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.ObjectContainer = this.Type;
            container.InitFromConfigFile = this.initFromConfigFile;
            container.SectionName = this.sectionName;
            return container;
        }
    }
}
