using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Plugin;

namespace EApp.Plugin.Generic
{
    public abstract class NonUIPluginAction : IPlugin
    {
        public event EventHandler<PluginLoadedEventArgs> Loaded;

        public event EventHandler Unloaded;

        public LifetimeMode Lifetime
        {
            get 
            { 
                return LifetimeMode.OnlyOnce; 
            }
        }

        public void Run(IPluginServiceProvider serviceProvider) 
        {
            bool cancelled = false;
            
            object actionResult = null;

            Exception exception = null;

            try
            {
                actionResult = this.RunCore(serviceProvider);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                this.OnLoaded(exception, cancelled, actionResult);
            }
        }

        public void Unload()
        {
            this.UnloadCore();

            this.OnUnloaded();
        }

        protected abstract object RunCore(IPluginServiceProvider serviceProvider);

        protected abstract void UnloadCore();

        protected virtual void OnLoaded(Exception exception, bool cancelled, object actionResult)
        {
            if (this.Loaded != null)
            {
                this.Loaded(this, new PluginLoadedEventArgs(exception, cancelled, actionResult));
            }
        }

        public virtual void OnUnloaded()
        {
            if (this.Unloaded != null)
            {
                this.Unloaded(this, EventArgs.Empty);
            }
        }
    }
}
