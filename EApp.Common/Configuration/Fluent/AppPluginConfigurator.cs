using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Configuration.Fluent
{
    public class AppPluginConfigurator : ConfigSourceConfigurator
    {
        private Type type; 
        
        private string name;

        public AppPluginConfigurator(IConfigurator<RegularConfigSource> context, Type type, string name) : base(context)
        {
            this.type = type;
            this.name = name;
        }

        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.AddAppPlugin(this.type, this.name);

            return container;
        }
    }
}
