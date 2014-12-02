using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Configuration.Fluent
{

    public class ApplicationConfigurator : TypeSpecifiedConfigSourceConfigurator
    {
        public ApplicationConfigurator(IConfigurator<RegularConfigSource> context, Type type)
            : base(context, type)
        {
        
        }

        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.Application = this.Type;
            return container;
        }
    }
}
