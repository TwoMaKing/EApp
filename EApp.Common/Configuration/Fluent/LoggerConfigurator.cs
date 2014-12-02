using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Configuration.Fluent
{
    public class LoggerConfigurator : TypeSpecifiedConfigSourceConfigurator
    {
        public LoggerConfigurator(IConfigurator<RegularConfigSource> context, Type type) : base(context, type) 
        { 
        
        }

        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.Logger = this.Type;
            return container;
        }
    }
}
