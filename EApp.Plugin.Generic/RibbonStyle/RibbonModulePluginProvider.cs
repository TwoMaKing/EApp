using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;

namespace EApp.Plugin.Generic.RibbonStyle
{
    public class RibbonModulePluginProvider : IPluginProvider
    {
        public const string Ribbon_Separator = "|";

        private static List<NavigationNodeItem> navigationNodeList = new List<NavigationNodeItem>();

        private static SortedList<NavigationNodeItem, IEnumerable<RibbonModulePluginItem>> pluginsByNavigation = 
            new SortedList<NavigationNodeItem, IEnumerable<RibbonModulePluginItem>>();

        public RibbonModulePluginProvider() { }

        public IDictionary<NavigationNodeItem, IEnumerable<TPluginItem>> GetPlugins<TPluginItem>() where TPluginItem : PluginItem<TPluginItem>, new()
        {
            return (IDictionary<NavigationNodeItem, IEnumerable<TPluginItem>>)pluginsByNavigation;
        }

        protected static List<NavigationNodeItem> NavigationNodeList 
        {
            get
            {
                return navigationNodeList;
            }
        }

        protected static SortedList<NavigationNodeItem, IEnumerable<RibbonModulePluginItem>> PluginsByNavigation
        {
            get
            {
                return pluginsByNavigation;
            }
        }

        public static RibbonModulePluginItem GetRibbonModulePluginItem(string navigationName, string pluginItemName)
        {
             return pluginsByNavigation.SingleOrDefault(k => k.Key.Name.Equals(navigationName)).
                    Value.SingleOrDefault(v=>v.Name.Equals(pluginItemName));
        }

        #region Create Xpress Module Plugin Items 

        public static TRibbonModulePluginItem CreateXpressModulePluginItem<TRibbonModulePluginItem>(string name,
                                                                                                     string text,
                                                                                                     string icon,
                                                                                                     string tooltip,
                                                                                                     NavigationNodeItem navigation)
            where TRibbonModulePluginItem : RibbonModulePluginItem, new()
        {
            return CreateXpressModulePluginItem<TRibbonModulePluginItem>(name, text, icon, tooltip, true, navigation);
        }

        public static TRibbonModulePluginItem CreateXpressModulePluginItem<TRibbonModulePluginItem>(string name,
                                                                                                   string text,
                                                                                                   string icon,
                                                                                                   string tooltip,
                                                                                                   RibbonButtonAlignment alignment,
                                                                                                   NavigationNodeItem navigation)
            where TRibbonModulePluginItem : RibbonModulePluginItem, new()
        {
            return CreateXpressModulePluginItem<TRibbonModulePluginItem>(name, text, icon, tooltip, true, alignment, navigation);
        }

        public static TRibbonModulePluginItem CreateXpressModulePluginItem<TRibbonModulePluginItem>(string name,
                                                                                                      string text,
                                                                                                      string icon,
                                                                                                      string tooltip,
                                                                                                      bool enabled,
                                                                                                      NavigationNodeItem navigation)
            where TRibbonModulePluginItem : RibbonModulePluginItem, new()
        {
            return CreateXpressModulePluginItem<TRibbonModulePluginItem>(name, text, icon, tooltip, enabled, false, navigation);
        }

        public static TRibbonModulePluginItem CreateXpressModulePluginItem<TRibbonModulePluginItem>(string name,
                                                                                                      string text,
                                                                                                      string icon,
                                                                                                      string tooltip,
                                                                                                      bool enabled,
                                                                                                      RibbonButtonAlignment alignment,
                                                                                                      NavigationNodeItem navigation)
            where TRibbonModulePluginItem : RibbonModulePluginItem, new()
        {
            return CreateXpressModulePluginItem<TRibbonModulePluginItem>(name, text, icon, tooltip, true, enabled, false, alignment, navigation);
        }

        public static TRibbonModulePluginItem CreateXpressModulePluginItem<TRibbonModulePluginItem>(string name,
                                                                                                      string text,
                                                                                                      string icon,
                                                                                                      string tooltip,
                                                                                                      bool enabled,
                                                                                                      bool isChecked,
                                                                                                      NavigationNodeItem navigation)
            where TRibbonModulePluginItem : RibbonModulePluginItem, new()
        {
            return CreateXpressModulePluginItem<TRibbonModulePluginItem>(name, text, icon, tooltip, true, enabled, isChecked, RibbonButtonAlignment.Left, navigation);
        }

        public static TRibbonModulePluginItem CreateXpressModulePluginItem<TRibbonModulePluginItem>(string name,
                                                                                                    string text,
                                                                                                    string icon,
                                                                                                    string tooltip,
                                                                                                    bool visible,
                                                                                                    bool enabled,
                                                                                                    bool isChecked,
                                                                                                    RibbonButtonAlignment alignment,
                                                                                                    NavigationNodeItem navigation)
            where TRibbonModulePluginItem : RibbonModulePluginItem, new()
        {
            TRibbonModulePluginItem pluginItem = new TRibbonModulePluginItem();

            pluginItem.Name = name;
            pluginItem.Text = text;
            pluginItem.Tooltip = tooltip;
            pluginItem.Navigation = navigation;
            pluginItem.IconResourceName = icon;
            pluginItem.Tooltip = tooltip;
            pluginItem.Visible = visible;
            pluginItem.Enabled = enabled;
            pluginItem.Checked = isChecked;
            pluginItem.Alignment = alignment;
            
            return pluginItem;
        }

        #endregion
    }
}
