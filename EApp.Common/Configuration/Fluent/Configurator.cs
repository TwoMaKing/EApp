using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Configuration.Fluent
{
    public abstract class Configurator<TContainer> : IConfigurator<TContainer>
    {
        private IConfigurator<TContainer> context;

        public Configurator(IConfigurator<TContainer> context)
        {
            this.context = context;
        }

        public IConfigurator<TContainer> configurator 
        {
            get 
            {
                return this.context;
            }
        }

        protected abstract TContainer DoConfigure(TContainer container);

        public TContainer Configure()
        {
            var container = this.context.Configure();

            return this.DoConfigure(container);
        }
    }
}
