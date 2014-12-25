using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Plugin;

namespace EApp.Plugin.Generic
{
    public class PluginController<TPluginItem> : IPluginController<TPluginItem> where TPluginItem : PluginItem<TPluginItem>, new()
    {
        public event CancelEventHandler Loading;

        public event EventHandler<PluginLoadedEventArgs> Loaded;

        public event CancelEventHandler Unloading;

        public event EventHandler Unloaded;

        private bool running;

        private TPluginItem pluginItem;

        private IPluginServiceProvider serviceProvider;

        private IPlugin pluginInstance;

        protected IPluginManager<TPluginItem> pluginManager;

        public PluginController(TPluginItem pluginItem, IPluginServiceProvider serviceProvider, IPluginManager<TPluginItem> pluginManager) 
        {
            this.pluginItem = pluginItem;
            
            this.serviceProvider = serviceProvider;

            this.pluginManager = pluginManager;
        }

        public TPluginItem PluginItem
        {
            get 
            {
                return this.pluginItem;
            }
        }

        public IPlugin PluginInstance
        {
            get 
            {
                return pluginInstance;
            }
        }

        public IPluginServiceProvider ServiceProvider
        {
            get 
            {
                return this.serviceProvider;
            }
        }

        public IPluginManager<TPluginItem> PluginManager
        {
            get 
            { 
                throw new NotImplementedException(); 
            }
        }

        public bool Running
        {
            get
            {
                return this.running;
            }
        }

        public void Load()
        {
            if (!OnPluginLoading())
            {
                if (this.PluginInstance == null)
                {
                    this.pluginInstance = this.CreatePluginInstance();

                    this.pluginInstance.Loaded += new EventHandler<PluginLoadedEventArgs>(this.PluginInstanceLoaded);
                    this.pluginInstance.Unloaded += new EventHandler(this.PluginInstanceUnloaded);
                }

                //Before running new plugin stop the current running pulgin.
                this.StopCurrentRunningPluginController();

                // To run new plugin.
                this.serviceProvider.AddService(typeof(IPluginController<>), this);

                this.pluginInstance.Run(this.serviceProvider);

                if (this.pluginInstance is IView)
                {
                    ((IView)this.pluginInstance).RefreshView();
                }

                this.running = this.pluginInstance.Lifetime == LifetimeMode.KeepAlive;
            }
        }

        public void Unload(bool disposing)
        {
            if (this.OnPluginInstanceUnloading())
            {
                return;
            }

            if (this.PluginInstance != null)
            {
                this.pluginInstance.Unload();

                if (disposing)
                {
                    this.pluginInstance = null;
                }

                this.running = false;
            }
        }

        #region protected methods

        protected virtual IPlugin CreatePluginInstance() 
        {
            return (IPlugin)EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve<IPlugin>(this.PluginItem.Name);
        }

        protected virtual bool OnPluginLoading() 
        {
            if (this.Loading != null)
            {
                CancelEventArgs e = new CancelEventArgs();
                this.Loading(this, e);

                return e.Cancel;
            }

            return false;
        }

        protected virtual void PluginInstanceLoaded(object sender, PluginLoadedEventArgs e) 
        {
            if (this.Loaded != null)
            {
                this.Loaded(this, e); 
            }
        }

        protected virtual bool OnPluginInstanceUnloading() 
        {
            if (this.Unloading != null)
            {
                CancelEventArgs e = new CancelEventArgs();
                this.Unloading(this, e);

                return e.Cancel;
            }

            return false;
        }

        protected virtual void PluginInstanceUnloaded(object sender, EventArgs e) 
        {
            if (this.Unloaded != null)
            {
                this.Unloaded(this, e);
            }
        }

        protected virtual void StopCurrentRunningPluginController() 
        {
            IPluginController<TPluginItem> currentRunningPluginController =
                this.pluginManager.PluginControllers.RunningPluginController;

            if (currentRunningPluginController == null)
            {
                return;
            }

            if (this.PluginInstance.Lifetime == LifetimeMode.KeepAlive)
            {
                currentRunningPluginController.Unload(false);
            }
        }

        #endregion

    }
}
