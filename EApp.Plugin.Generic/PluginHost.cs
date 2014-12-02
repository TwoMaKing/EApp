using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Plugin;

namespace EApp.Plugin.Generic
{
    public abstract class PluginHost<TServiceProviderFactory, TPluginItem> : IHost 
        where TServiceProviderFactory : IPluginServiceProviderFactory, new()
        where TPluginItem : PluginItem<TPluginItem>, new()
    {
        private IPluginProvider pluginProvider;

        private TServiceProviderFactory serviceProvider = default(TServiceProviderFactory);

        private IPluginManager<TPluginItem> pluginManager;

        public PluginHost(IPluginProvider pluginProvider, TServiceProviderFactory serviceProver) 
        {
            this.serviceProvider = serviceProver;

            this.pluginProvider = pluginProvider;

            this.pluginManager = new PluginManager<TPluginItem>(this.pluginProvider, this.serviceProvider.ServiceProvider);

            this.pluginManager.PluginAdded -= new EventHandler<EventArgs<TPluginItem>>(this.OnPluginAdded);
            this.pluginManager.PluginAdded += new EventHandler<EventArgs<TPluginItem>>(this.OnPluginAdded);
        }

        public TServiceProviderFactory ServiceProvider
        {
            get 
            {
                return this.serviceProvider;   
            }
        }

        public IPluginProvider PluginProvider
        {
            get 
            { 
                return this.pluginProvider; 
            }
        }

        IPluginServiceProviderFactory IHost.ServiceProvider
        {
            get 
            {
                return this.serviceProvider;
            }
        }

        public void Run()
        {
            this.pluginManager.Initialize();

            this.RunCore();
        }

        public abstract void Exit();

        protected IPluginManager<TPluginItem> PluginManager 
        {
            get 
            {
                return this.pluginManager;
            }
        } 

        protected abstract void RunCore();

        protected abstract void OnPluginAdded(object sender, EventArgs<TPluginItem> e);

    }
}
