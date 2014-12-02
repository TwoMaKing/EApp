using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.List;
using EApp.Core.Plugin;

namespace EApp.Plugin.Generic
{
    public class PluginManager<TPluginItem> : IPluginManager<TPluginItem> where TPluginItem : PluginItem<TPluginItem>, new()
    {
        public event EventHandler<CancelEventArgs<TPluginItem>> PluginAdding;

        public event EventHandler<EventArgs<TPluginItem>> PluginAdded;

        public event EventHandler<CancelEventArgs<TPluginItem>> PluginRemoving;

        public event EventHandler<EventArgs<TPluginItem>> PluginRemoved;

        private IPluginProvider pluginProvider;

        private IPluginServiceProvider serviceProvider;

        private PluginControllerCollection<TPluginItem> pluginControllers;

        public PluginManager(IPluginProvider pluginProvider, IPluginServiceProvider serviceProvider)
        {
            this.pluginProvider = pluginProvider;

            this.serviceProvider = serviceProvider;

            this.pluginControllers = new PluginControllerCollection<TPluginItem>();

            this.serviceProvider.AddService(typeof(IPluginManager<>), this);
        }

        public PluginControllerCollection<TPluginItem> PluginControllers
        {
            get 
            {
                return this.pluginControllers;
            }
            protected set
            {
                this.pluginControllers = value;
            }
        }

        public void Initialize()
        {
            this.CreatePluginControllers();
        }

        public void Refresh()
        {
            this.Initialize();
        }

        public void RegisterPlugin(TPluginItem item)
        {
            return;
        }

        public void DisposePlugin(TPluginItem item)
        {
            if (this.PluginControllers == null ||
                item == null)
            {
                return;
            }

            bool existingPluginInstance = this.PluginControllers.Exists(p => item.Name.Equals(p.PluginItem.Name, 
                                                                        StringComparison.InvariantCultureIgnoreCase));
            if (existingPluginInstance)
            {
                this.PluginControllers[item.Name].Unload(true);
            }
        }

        protected virtual void CreatePluginControllers() 
        {
            this.PluginControllers.Clear();

            IDictionary<NavigationNodeItem, IEnumerable<TPluginItem>> pluginItemsByNavigation = this.pluginProvider.GetPlugins<TPluginItem>();

            if (pluginItemsByNavigation == null)
            {
                return;
            }

            foreach (NavigationNodeItem navigation in pluginItemsByNavigation.Keys) 
            {
                IEnumerable<TPluginItem> pluginItems = pluginItemsByNavigation[navigation];

                if (pluginItems == null)
                {
                    continue;
                }

                this.CreatePluginControllers(pluginItems);
            }
        }

        /// <summary>
        /// Controllers recursion creation.
        /// </summary>
        protected virtual void CreatePluginControllers(IEnumerable<TPluginItem> pluginItems)
        {
            if (pluginItems == null)
            {
                return;
            }

            foreach (TPluginItem pluginItem in pluginItems)
            {
                if (!this.OnPluginAdding(pluginItem))
                {
                    IPluginController<TPluginItem> pluginController = this.CreatePluginController(pluginItem);

                    if (pluginController != null)
                    {
                        this.PluginControllers.Add(pluginController);

                        this.OnPluginAdded(pluginItem);
                    }
                }

                this.CreatePluginControllers(pluginItem.SubItems);                
            }
        }

        protected virtual IPluginController<TPluginItem> CreatePluginController(TPluginItem pluginItem) 
        {
            IPluginController<TPluginItem> pluginController = new PluginController<TPluginItem>(pluginItem, this.serviceProvider, this);

            return pluginController;
        }

        protected virtual bool OnPluginAdding(TPluginItem pluginItem)
        {
            if (this.PluginAdding != null)
            {
                CancelEventArgs<TPluginItem> e = new CancelEventArgs<TPluginItem>(pluginItem);

                return e.Cancel;
            }

            return false;
        }

        protected virtual void OnPluginAdded(TPluginItem pluginItem)
        {
            if (this.PluginAdded != null)
            {
                EventArgs<TPluginItem> e = new EventArgs<TPluginItem>(pluginItem);
                this.PluginAdded(this, e);
            }
        }

        protected virtual bool OnPluginRemoving(TPluginItem pluginItem)
        {
            if (this.PluginRemoving != null)
            {
                CancelEventArgs<TPluginItem> e = new CancelEventArgs<TPluginItem>(pluginItem);

                return e.Cancel;
            }

            return false;
        }

        protected virtual void OnPluginRemoved(TPluginItem pluginItem)
        {
            if (this.PluginRemoved != null)
            {
                EventArgs<TPluginItem> e = new EventArgs<TPluginItem>(pluginItem);
                this.PluginRemoved(this, e);
            }
        }

    }
}
