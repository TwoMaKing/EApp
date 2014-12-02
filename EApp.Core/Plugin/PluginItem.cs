using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using EApp.Core.List;

namespace EApp.Core.Plugin
{
    /// <summary>
    /// The description (meta data) of the plugin item. 
    /// </summary>
    public abstract class PluginItem<TPluginItem> : IHierarchicalEntity<TPluginItem, PluginItemCollection<TPluginItem>> 
        where TPluginItem : PluginItem<TPluginItem>, new()
    {
        private string name = string.Empty;
        private string text = string.Empty;
        private string tooltip = string.Empty;
        private Type providerType;
        private NavigationNodeItem navigationNode;
        private TPluginItem parent;
        private PluginItemCollection<TPluginItem> subItems = new PluginItemCollection<TPluginItem>();

        public PluginItem() : this(string.Empty, null) { }
            
        public PluginItem(string name, NavigationNodeItem navigationNode)
            : this(name, string.Empty, navigationNode)
        {
            
        }

        public PluginItem(string name, string text, NavigationNodeItem navigationNode)
            : this(name, text, string.Empty, navigationNode)
        {

        }

        public PluginItem(string name, string text, string tooltip, NavigationNodeItem navigationNode) :
            this(name, text, tooltip, null, navigationNode)
        {

        }

        public PluginItem(string name, string text, string tooltip, Type providerType, NavigationNodeItem navigationNode)
        {
            this.name = name;
            this.text = text;
            this.tooltip = tooltip;
            this.providerType = providerType;
            this.navigationNode = navigationNode;

            this.subItems.ArrayListChanged += 
                new NotifyCollectionChangedEventHandler(delegate(object sender, NotifyCollectionChangedEventArgs e) 
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                    {
                        if (e.NewItems != null)
                        {
                            foreach (object newPluginItemObject in e.NewItems) 
                            {
                                TPluginItem p = (TPluginItem)newPluginItemObject;

                                p.parent = (TPluginItem)this;
                            }
                        }
                    }
                });
        }

        public string Name 
        {
            get 
            {
                return this.name;    
            }
            set
            {
                this.name = value;
            }
        }

        public string Text 
        {
            get 
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        public string Tooltip 
        {
            get 
            {
                return this.tooltip;
            }
            set
            {
                this.tooltip = value;
            }
        }

        public Type ProviderType 
        {
            get 
            {
                return this.providerType;
            }
            set
            {
                this.providerType = value;
            }
        }

        public NavigationNodeItem Navigation
        {
            get 
            {
                return this.navigationNode;
            }
            set
            {
                this.navigationNode = value;
            }
        }

        public TPluginItem Parent
        {
            get 
            {
                return this.parent;
            }
        }

        public PluginItemCollection<TPluginItem> SubItems
        {
            get 
            {
                return this.subItems;
            }
        }


        #region Create Xpress Module Plugin Items

        public static TPluginItem CreatePluginItem(string name,
                                                   string text,
                                                   string icon,
                                                   string tooltip,
                                                   NavigationNodeItem navigation)
        {
            TPluginItem pluginItem = new TPluginItem();

            pluginItem.Name = name;
            pluginItem.Text = text;
            pluginItem.Tooltip = tooltip;
            pluginItem.Navigation = navigation;

            return pluginItem;
        }

        #endregion


    }
}
