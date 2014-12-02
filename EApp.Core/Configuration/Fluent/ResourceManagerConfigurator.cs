using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Configuration.Fluent
{
    public class ResourceManagerConfigurator : ConfigSourceConfigurator
    {
        private Type resourceType;

        private string resourceName;

        public ResourceManagerConfigurator(IConfigurator<RegularConfigSource> context, Type type, string name) : base(context)
        {
            this.resourceType = type;

            this.resourceName = name;
        }

        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.AddResourceManager(this.resourceType, this.resourceName);

            return container;
        }
    }
}
