using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Configuration.Fluent
{
    public class MiscSettingConfigurator : ConfigSourceConfigurator
    {
        private string key;

        private string value;

        public MiscSettingConfigurator(IConfigurator<RegularConfigSource> context, string key, string value) :base(context)
        {
            this.key = key;

            this.value = value;
        }

        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.AddMiscSetting(key, value);

            return container;
        }
    }
}
