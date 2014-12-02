using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Configuration.Fluent
{
    public abstract class TypeSpecifiedConfigSourceConfigurator : ConfigSourceConfigurator
    {
        private Type type;

        public TypeSpecifiedConfigSourceConfigurator(IConfigurator<RegularConfigSource> context, Type type)
            : base(context)
        {
            this.type = type;
        }

        public Type Type 
        {
            get 
            {
                return this.type;
            }
        }

    }
}
