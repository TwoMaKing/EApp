using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Core.Application;
using EApp.UI.Controls.UIHandler;

namespace EApp.Plugin.Generic.RibbonStyle
{
    public static class RibbonExtensionHelper
    {
        public static void AddRibbonButton(Ribbon ribbon, 
                                           string tabText, 
                                           string buttonName,
                                           string text, 
                                           Image image, 
                                           string tooltip, 
                                           bool enable, 
                                           bool isChecked,
                                           RibbonButtonAlignment alignment,
                                           EventHandler clickHandler)
        {
            if (ribbon == null)
            {
                return;
            }

            RibbonItem ribbonItem = null;

            if (RibbonModulePluginProvider.Ribbon_Separator.Equals(buttonName))
            {
                ribbonItem = new RibbonSeparator();
            }
            else
            {
                ribbonItem = new RibbonButton();
                ribbonItem.Text = text;
                ribbonItem.Image = image;
                ribbonItem.ToolTip = tooltip;
                ribbonItem.Enabled = enable;
                ribbonItem.Checked = isChecked;
                ribbonItem.Click += new EventHandler(clickHandler);
            }

            ribbonItem.Name = buttonName;
            
            RibbonTab ribbonTab = RibbonHelper.FindRibbonTab(ribbon, tabText);

            if (ribbonTab == null)
            {
                return;
            }

            int panelIndex = alignment == RibbonButtonAlignment.Right ? 1 : 0;

            ribbonTab.Panels[panelIndex].Items.Add(ribbonItem);
        }

        public static void AddRibbonButton(Ribbon ribbon,
                                           RibbonModulePluginItem modulePluginItem,
                                           EventHandler clickHandler)
        {
            if (ribbon == null ||
                modulePluginItem == null)
            {
                return;
            }

            Image buttonImage = EAppRuntime.Instance.App.ResourceManagers["Common"].GetImage(modulePluginItem.IconResourceName) ;

            AddRibbonButton(ribbon,
                            modulePluginItem.Navigation.Text,
                            modulePluginItem.Name,
                            modulePluginItem.Text,
                            buttonImage,
                            modulePluginItem.Tooltip,
                            modulePluginItem.Enabled,
                            modulePluginItem.Checked,
                            modulePluginItem.Alignment,
                            clickHandler);
        }

        public static void RemoveRibbonButton(Ribbon ribbon, RibbonModulePluginItem modulePluginItem)
        {
            if (ribbon == null)
            {
                return;
            }

            RibbonTab ribbonTab = RibbonHelper.FindRibbonTab(ribbon, modulePluginItem.Navigation.Text);

            if (ribbonTab == null)
            {
                return;
            }

            RibbonItem ribbonItem = RibbonHelper.FindRibbonItem(ribbon, 
                                                                modulePluginItem.Navigation.Text, 
                                                                modulePluginItem.Name);

            if (ribbonItem == null)
            {
                return;
            }

            int panelIndex = modulePluginItem.Alignment == RibbonButtonAlignment.Right ? 1 : 0;

            ribbonTab.Panels[panelIndex].Items.Remove(ribbonItem);
        }

        public static void SetSingleRibbonButtonToCheckedStatus(Ribbon ribbon, string tabText, string buttonName)
        {
            if (ribbon == null ||
                ribbon.Tabs == null ||
                ribbon.Tabs.Count.Equals(0) ||
                string.IsNullOrEmpty(tabText) ||
                string.IsNullOrEmpty(buttonName))
            {
                return;
            }

            RibbonModulePluginItem modulePluginItem = null;

            foreach (RibbonTab tab in ribbon.Tabs)
            {
                foreach (RibbonPanel panel in tab.Panels)
                {
                    foreach (RibbonItem item in panel.Items)
                    {
                        modulePluginItem = RibbonModulePluginProvider.GetRibbonModulePluginItem(tab.Name, item.Name);
                        
                        if (modulePluginItem != null &&
                            modulePluginItem.ClickStatus == RibbonButtonClickStatus.ToggledCheckedOnClick)
                        {
                            continue;
                        }

                        item.Checked = tabText.Equals(tab.Text) &&
                                       buttonName.Equals(item.Name);
                    }
                }
            }
        }

        public static void SetAllRibbonButtonsToUncheckedStatus(Ribbon ribbon)
        {
            if (ribbon == null ||
                ribbon.Tabs == null ||
                ribbon.Tabs.Count.Equals(0))
            {
                return;
            }

            RibbonModulePluginItem modulePluginItem = null;

            foreach (RibbonTab tab in ribbon.Tabs)
            {
                foreach (RibbonPanel panel in tab.Panels)
                {
                    foreach (RibbonItem item in panel.Items)
                    {
                        modulePluginItem = RibbonModulePluginProvider.GetRibbonModulePluginItem(tab.Name, item.Name);

                        if (modulePluginItem != null &&
                            modulePluginItem.ClickStatus == RibbonButtonClickStatus.ToggledCheckedOnClick)
                        {
                            continue;
                        }

                        item.Checked = false;
                    }
                }
            }
        }
    }
}
