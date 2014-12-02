using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;

namespace EApp.Plugin.Generic.RibbonStyle
{
    public enum RibbonButtonAlignment
    {
        Left,

        Right
    }

    public enum RibbonButtonClickStatus
    {
        /// <summary>
        /// After clicking no checked status.
        /// </summary>
        NoCheckedOnClick,

        /// <summary>
        /// After clicking keep checked status.
        /// </summary>
        KeepCheckedOnClick,

        /// <summary>
        /// Toggled status. Clicking first time the buttin is checked and then click second time the button becomes unchecked.
        /// </summary>
        ToggledCheckedOnClick
    }

    public class RibbonModulePluginItem : PluginItem<RibbonModulePluginItem>
    {
        public RibbonModulePluginItem() : this(string.Empty, null) { }

        public RibbonModulePluginItem(string name, NavigationNodeItem navigationNode)
            : this(name, string.Empty, navigationNode)
        {

        }

        public RibbonModulePluginItem(string name, string text, NavigationNodeItem navigationNode) :
            this(name, text, string.Empty, navigationNode)
        {

        }

        public RibbonModulePluginItem(string name, string text, string tooltip, NavigationNodeItem navigationNode) :
            this(name, text, tooltip, null, navigationNode)
        {

        }

        public RibbonModulePluginItem(string name, string text, string tooltip, Type providerType, NavigationNodeItem navigationNode) :
            base(name, text, tooltip, providerType, navigationNode)
        {

        }

        public string IconResourceName { get; set; }

        public bool Visible { get; set; }

        public bool Enabled { get; set; }

        public bool Checked { get; set; }

        [DefaultValue(RibbonButtonAlignment.Left)]
        public RibbonButtonAlignment Alignment { get; set; }

        [DefaultValue(RibbonButtonClickStatus.NoCheckedOnClick)]
        public RibbonButtonClickStatus ClickStatus { get; set; }
    }
}
