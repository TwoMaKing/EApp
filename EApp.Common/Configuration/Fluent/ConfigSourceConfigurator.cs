using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Configuration.Fluent
{
    public abstract class ConfigSourceConfigurator : Configurator<RegularConfigSource>, IConfigSourceConfigurator
    {
        public ConfigSourceConfigurator(IConfigurator<RegularConfigSource> context) : base(context) { }
    }
}
