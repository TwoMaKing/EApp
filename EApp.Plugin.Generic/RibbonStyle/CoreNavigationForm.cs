using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using EApp.UI.Controls.UIHandler;

namespace EApp.Plugin.Generic.RibbonStyle
{
    public partial class CoreNavigationForm : Form, IPluginServiceProviderFactory
    {
        private IPluginProvider pluginProvider;

        private IResourceManager resourceManager;

        private Ribbon ribbonMenu;

        public CoreNavigationForm() 
        {
            InitializeComponent();
        }

        public CoreNavigationForm(IPluginProvider pluginProvider, IResourceManager resourceManager)
        {
            InitializeComponent();

            this.pluginProvider = pluginProvider;

            this.resourceManager = resourceManager;

            this.InitializeRibbonMenu();

            this.PopulateRibbonMenuButtons();
        }

        public Ribbon RibbonMenu
        {
            get 
            {
                return this.ribbonMenu;
            }
        }

        public IPluginProvider PluginProvider 
        {
            get 
            {
                return this.pluginProvider;
            }
        }

        public IResourceManager ResourceManager 
        {
            get 
            {
                return this.resourceManager;
            }
        }

        public virtual void RegisterRibbonButtonClickCommand(string modulePluginCommandName, EventHandler clickHandler) 
        {
            RibbonItem ribbonButton = RibbonHelper.FindRibbonItem(this.RibbonMenu, modulePluginCommandName);

            if (ribbonButton == null)
            {
                return;
            }

            ribbonButton.Click += clickHandler;
        }

        #region IPluginServiceProviderFactory members
        
        public virtual IPluginServiceProvider ServiceProvider
        {
            get 
            {
                IDictionary<Type, object> serviceTypes = new Dictionary<Type, object>();

                serviceTypes.Add(typeof(Form), this);
                serviceTypes.Add(typeof(Ribbon), this.RibbonMenu);

                PluginServiceProviderAdapter serviceProviderAdapter = new PluginServiceProviderAdapter(serviceTypes);

                return serviceProviderAdapter;
            }
        }

        #endregion

        #region Create Ribbon Menu

        protected virtual void InitializeRibbonMenu() 
        {
            this.ribbonMenu = new Ribbon();
            this.ribbonMenu.Minimized = false;
            this.ribbonMenu.Location = new System.Drawing.Point(0, 0);
            this.ribbonMenu.Size = new System.Drawing.Size(this.Width, 140);
            this.ribbonMenu.OrbDropDown.BorderRoundness = 8;
            this.ribbonMenu.OrbDropDown.Location = new System.Drawing.Point(0, 0);

            this.Controls.Add(this.ribbonMenu);
        }

        protected virtual void PopulateRibbonMenuButtons()
        {
            IDictionary<NavigationNodeItem, IEnumerable<RibbonModulePluginItem>> modulePluginsByNavList = 
                this.pluginProvider.GetPlugins<RibbonModulePluginItem>();

            this.ribbonMenu.Tabs.Clear();

            this.ribbonMenu.QuickAcessToolbar.Items.Clear();

            foreach (KeyValuePair<NavigationNodeItem, IEnumerable<RibbonModulePluginItem>> pluginItemsByNav in modulePluginsByNavList)
            {
                string tabName = pluginItemsByNav.Key.Name;
                string tabText = pluginItemsByNav.Key.Text;

                RibbonPanel mainModulePanel = new RibbonPanel();
                RibbonPanel subModulePanel = new RibbonPanel();

                IEnumerable<RibbonModulePluginItem> modulePlugins = pluginItemsByNav.Value;

                foreach (RibbonModulePluginItem modulePluginItem in modulePlugins)
                {
                    RibbonButton ribbonButton = new RibbonButton();

                    ribbonButton.Name = modulePluginItem.Name;
                    ribbonButton.Text = modulePluginItem.Text;
                    ribbonButton.Image = (Image)this.resourceManager.GetImage(modulePluginItem.IconResourceName);

                    ribbonButton.Enabled = modulePluginItem.Enabled;
                    ribbonButton.Checked = modulePluginItem.Checked;
                    ribbonButton.Tag = modulePluginItem;

                    if (modulePluginItem.Alignment == RibbonButtonAlignment.Left)
                    {
                        mainModulePanel.Items.Add(ribbonButton);
                    }
                    else
                    {
                        subModulePanel.Items.Add(ribbonButton);
                    }
                }

                RibbonTab ribbonTab = new RibbonTab();
                ribbonTab.Name = tabName;
                ribbonTab.Text = tabText;

                ribbonTab.Panels.AddRange(new RibbonPanel[] { mainModulePanel, subModulePanel });

                this.ribbonMenu.Tabs.Add(ribbonTab);
            }
        }

        #endregion
    }
}
