using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Common.Util;

namespace EApp.UI.Controls.UIHandler
{
    /// <summary>
    /// the class of Express Ribbon Menu Helper
    /// </summary>
    public sealed class RibbonHelper
    {
        /// <summary>
        /// Set Ribbon QuickAccessBar Item Enable Status
        /// </summary>
        /// <param name="ribbon">Express Ribbon</param>
        /// <param name="quickAccessItemName">QuickAccess Item Name</param>
        /// <param name="enabled">Ribbon Menu Item Enable</param>
        public static void SetRibbonQuickAccessBarItemEnableStatus(Ribbon ribbon, string quickAccessItemName, bool enabled)
        {
            if (ribbon == null ||
                ribbon.QuickAcessToolbar == null ||
                ribbon.QuickAcessToolbar.Items == null ||
                ribbon.QuickAcessToolbar.Items.Count.Equals(0))
            {
                return;
            }

            foreach (RibbonItem item in ribbon.QuickAcessToolbar.Items)
            {
                if (quickAccessItemName.Equals(item.Name))
                {
                    item.Enabled = enabled;
                    item.Checked = false;
                    item.SetSelected(false);
                    item.RedrawItem();
                    return;
                }
            }
        }

        /// <summary>
        /// set the ribbon item text
        /// </summary>
        /// <param name="ribbon">express Ribbon</param>
        /// <param name="ribbonTabText">express Ribbon Tab Text</param>
        /// <param name="ribbonItemName">express Ribbon Item Name</param>
        /// <param name="text">express Ribbon ItemT ext</param>
        public static void SetRibbonItemText(Ribbon ribbon, string ribbonTabText, string ribbonItemName, string text)
        {
            RibbonItem item = FindRibbonItem(ribbon, ribbonTabText, ribbonItemName);

            if (item == null)
            {
                return;
            }

            item.Text = text;
        }


        /// <summary>
        /// set the ribbon item toolTip
        /// </summary>
        /// <param name="ribbon">express Ribbon</param>
        /// <param name="ribbonTabText">express Ribbon Tab Text</param>
        /// <param name="ribbonItemName">express Ribbon Item Name</param>
        /// <param name="expressRibbonItemText">express Ribbon ItemT ext</param>
        public static void SetRibbonItemToolTip(Ribbon ribbon, string ribbonTabText, string ribbonItemName, string tooltip)
        {
            RibbonItem item = FindRibbonItem(ribbon, ribbonTabText, ribbonItemName);

            if (item == null)
            {
                return;
            }

            item.ToolTip = tooltip;
        }

        public static void SetRibbonItemTextToolTip(Ribbon ribbon, string ribbonTabText, string ribbonItemName, string text, string tooltip)
        {
            RibbonItem item = FindRibbonItem(ribbon, ribbonTabText, ribbonItemName);

            if (item == null)
            {
                return;
            }

            item.Text = text;
            item.ToolTip = tooltip;
        }

        /// <summary>
        /// set the ribbon item enable status
        /// </summary>
        /// <param name="ribbon">express Ribbon</param>
        /// <param name="ribbonTabText">express Ribbon Tab Text</param>
        /// <param name="ribbonItemName">express Ribbon Item Name</param>
        /// <param name="enabled">ribbon Menu Item Enable</param>
        public static void SetRibbonItemEnableStatus(Ribbon ribbon, string ribbonTabText, string ribbonItemName, bool enabled)
        {
            RibbonItem item = FindRibbonItem(ribbon, ribbonTabText, ribbonItemName);

            if (item == null)
            {
                return;
            }

            item.Enabled = enabled;
        }

        public static void SetRibbonItemEnableStatus(Ribbon ribbon, string ribbonTabText, string[] ribbonItemNames, bool enabled)
        {
            RibbonItem[] items = FindRibbonItems(ribbon, ribbonTabText, ribbonItemNames);

            if (items == null)
            {
                return;
            }

            foreach (RibbonItem item in items)
            {
                item.Enabled = enabled;
            }
        }

        /// <summary>
        /// set the ribbon item check status
        /// </summary>
        /// <param name="ribbon">express Ribbon</param>
        /// <param name="ribbonTabText">express Ribbon Tab Text</param>
        /// <param name="ribbonItemName">express Ribbon Item Name</param>
        /// <param name="checkedStatus">ribbon Menu Item Check Status</param>
        public static void SetRibbonItemCheckStatus(Ribbon ribbon, string ribbonTabText, string ribbonItemName, bool isChecked)
        {
            RibbonItem item = FindRibbonItem(ribbon, ribbonTabText, ribbonItemName);

            if (item == null)
            {
                return;
            }

            item.Checked = isChecked;
        }

        /// <summary>
        /// Judge the active tab of Express Ribbon
        /// </summary>
        /// <param name="ribbon">express Ribbon</param>
        /// <param name="ribbonTabText">express Ribbon Tab Text</param>
        /// <returns>express Ribbon Tab</returns>
        public static bool IsActiveRibbonTab(Ribbon ribbon, string ribbonTabText)
        {
            if (ribbon == null ||
                ribbon.Tabs == null ||
                ribbon.Tabs.Count.Equals(0) ||
                ribbon.ActiveTab == null)
            {
                return false;
            }

            return ribbonTabText.Trim().Equals(ribbon.ActiveTab.Text.Trim());
        }

        /// <summary>
        /// set the active ribbon tab
        /// </summary>
        /// <param name="ribbon">express Ribbon</param>
        /// <param name="ribbonTabText">express Ribbon Tab Text</param>
        public static void SetActiveRibbonTab(Ribbon ribbon, string ribbonTabText)
        {
            if (ribbon == null ||
                ribbon.Tabs == null ||
                ribbon.Tabs.Count.Equals(0))
            {
                return;
            }

            foreach (RibbonTab tab in ribbon.Tabs)
            {
                if (ribbonTabText.Trim().Equals(tab.Text.Trim()))
                {
                    ribbon.ActiveTab = tab;
                    break;
                }
            }
        }

        public static void SetRibbonTabEnableStatus(Ribbon ribbon, string ribbonTabText, bool enabled)
        {
            RibbonTab tab = FindRibbonTab(ribbon, ribbonTabText);

            if (tab == null)
                return;

            foreach (RibbonPanel panel in tab.Panels)
            {
                panel.Enabled = enabled;
            }
        }

        public static void SetRibbonEnableStatus(Ribbon ribbon, bool enabled)
        {
            foreach (RibbonTab tab in ribbon.Tabs)
            {
                foreach (RibbonPanel panel in tab.Panels)
                {
                    panel.Enabled = enabled;
                }
            }
        }

        /// <summary>
        /// set the ribbon item image
        /// </summary>
        /// <param name="expressRibbon">express Ribbon</param>
        /// <param name="expressRibbonTabText">express Ribbon Tab Text</param>
        /// <param name="expressRibbonItemName">express Ribbon Item Name</param>
        /// <param name="expressRibbonItemImage">express Ribbon Item Image</param>
        public static void SetRibbonButtonImage(Ribbon ribbon, string ribbonTabText, string ribbonItemName, Image image)
        {
            RibbonItem item = FindRibbonItem(ribbon, ribbonTabText, ribbonItemName);

            if (item == null)
            {
                return;
            }

            item.Image = image;
            item.RedrawItem();

            ribbon.Refresh();
        }

        public static RibbonTab FindRibbonTab(Ribbon ribbon, string ribbonTabText)
        {
            if (ribbon == null ||
                ribbon.Tabs == null ||
                ribbon.Tabs.Count.Equals(0) ||
                string.IsNullOrEmpty(ribbonTabText))
            {
                return null;
            }

            foreach (RibbonTab tab in ribbon.Tabs)
            {
                if (ribbonTabText.Trim().Equals(tab.Text.Trim()))
                {
                    return tab;
                }
            }

            return null;
        }

        /// <summary>
        /// Find Ribbon Item
        /// </summary>
        /// <param name="ribbon">express Ribbon</param>
        /// <param name="ribbonTabText">express Ribbon Tab Text</param>
        /// <param name="ribbonItemName">express Ribbon Item Name</param>
        /// <returns>RibbonItem</returns>
        public static RibbonItem FindRibbonItem(Ribbon ribbon, string ribbonTabText, string ribbonItemName)
        {
            if (ribbon == null ||
                ribbon.Tabs == null ||
                ribbon.Tabs.Count.Equals(0) ||
                string.IsNullOrEmpty(ribbonTabText) ||
                string.IsNullOrEmpty(ribbonItemName))
            {
                return null;
            }

            foreach (RibbonTab tab in ribbon.Tabs)
            {
                if (ribbonTabText.Trim().Equals(tab.Text.Trim()))
                {
                    foreach (RibbonPanel panel in tab.Panels)
                    {
                        foreach (RibbonItem item in panel.Items)
                        {
                            if (ribbonItemName.Equals(item.Name))
                            {
                                return item;
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Find Ribbon Item
        /// </summary>
        /// <param name="ribbon">Ribbon</param>
        /// <param name="ribbonItemName">Ribbon Item Name</param>
        /// <returns>RibbonItem</returns>
        public static RibbonItem FindRibbonItem(Ribbon ribbon, string ribbonItemName)
        {
            if (ribbon == null ||
               ((ribbon.Tabs == null ||
                 ribbon.Tabs.Count.Equals(0)) &&
                (ribbon.QuickAcessToolbar == null ||
                 ribbon.QuickAcessToolbar.Items.Count.Equals(0)) && 
                (ribbon.OrbDropDown == null ||
                 ribbon.OrbDropDown.MenuItems.Count.Equals(0))) ||
                string.IsNullOrEmpty(ribbonItemName))
            {
                return null;
            }

            if (ribbon.OrbDropDown != null &&
                ribbon.OrbDropDown.MenuItems != null)
            {
                foreach (RibbonItem item in ribbon.OrbDropDown.MenuItems)
                {
                    if (ribbonItemName.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return item;
                    }
                }
            }

            if (ribbon.QuickAcessToolbar != null &&
                ribbon.QuickAcessToolbar.Items != null)
            {
                foreach (RibbonItem item in ribbon.QuickAcessToolbar.Items)
                {
                    if (ribbonItemName.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return item;
                    }
                }
            }

            foreach (RibbonTab tab in ribbon.Tabs)
            {
                foreach (RibbonPanel panel in tab.Panels)
                {
                    foreach (RibbonItem item in panel.Items)
                    {
                        if (ribbonItemName.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return item;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Find the array of Ribbon Item
        /// </summary>
        /// <param name="ribbon">Ribbon</param>
        /// <param name="ribbonTabText">Ribbon Tab Text</param>
        /// <param name="ribbonItemName">Array of Ribbon Item Name</param>
        /// <returns>RibbonItem</returns>
        private static RibbonItem[] FindRibbonItems(Ribbon ribbon, string ribbonTabText, string[] ribbonItemNames)
        {
            if (ribbon == null ||
                ribbon.Tabs == null ||
                ribbon.Tabs.Count.Equals(0) ||
                string.IsNullOrEmpty(ribbonTabText) ||
                ribbonItemNames == null)
            {
                return null;
            }

            List<RibbonItem> ribbonItemList = new List<RibbonItem>();

            foreach (RibbonTab tab in ribbon.Tabs)
            {
                if (ribbonTabText.Trim().Equals(tab.Text.Trim()))
                {
                    foreach (RibbonPanel panel in tab.Panels)
                    {
                        foreach (RibbonItem item in panel.Items)
                        {
                            if (ArrayUtils.IsExistInArray<string>(ribbonItemNames, item.Name))
                            {
                                ribbonItemList.Add(item);
                            }
                        }
                    }

                    break;
                }
            }

            return ribbonItemList.ToArray();
        }

    }
}
