using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using EApp.Common.List;

namespace EApp.UI.Plugin
{
    /// <summary>
    /// The description (meta data) of the plugin item. 
    /// </summary>
    public abstract class PluginItem<TPluginItem> : IHierarchicalEntity<TPluginItem, PluginItemCollection<TPluginItem>> 
        where TPluginItem : PluginItem<TPluginItem>
    {
        private string name = string.Empty;
        private string text = string.Empty;
        private string tooltip = string.Empty;
        private Type providerType;
        private NavigationNodeItem navigationNode;
        private TPluginItem parent;
        private PluginItemCollection<TPluginItem> subItems = new PluginItemCollection<TPluginItem>();

        public PluginItem(string name, NavigationNodeItem navigationNode)
            : this(name, string.Empty, navigationNode)
        {
            
        }

        public PluginItem(string name, string text, NavigationNodeItem navigationNode) :
            this(name, text, null, navigationNode)
        {

        }

        public PluginItem(string name, string text, Type providerType, NavigationNodeItem navigationNode)
        {
            this.name = name;
            this.text = text;
            this.providerType = providerType;
            this.navigationNode = navigationNode;
        }

        public string Name 
        {
            get 
            {
                return this.name;    
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
    }
}
