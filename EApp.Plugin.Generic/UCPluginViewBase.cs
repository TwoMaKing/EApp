using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Core;
using EApp.Core.Plugin;
using EApp.Core.Exceptions;

namespace EApp.Plugin.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPluginItem"></typeparam>
    public partial class UCPluginViewBase<TPluginItem> : UserControl, IPlugin, IViewService<TPluginItem>, IView
        where TPluginItem : PluginItem<TPluginItem>, new()
    {
        #region Private Properties

        private Form coreForm;

        private Control mdiUIContainer;

        private IPluginController<TPluginItem> currentModulePluginController;

        private TPluginItem currentAppModulePluginItem;

        private IPluginManager<TPluginItem> modulePluginManager;

        #endregion

        public UCPluginViewBase()
        {
            InitializeComponent();
        }

        #region IModulePlugin members

        public event EventHandler<PluginLoadedEventArgs> Loaded;

        public event EventHandler Unloaded;

        public LifetimeMode Lifetime
        {
            get 
            {
                return LifetimeMode.KeepAlive;
            }
        }

        public virtual void Run(IPluginServiceProvider serviceProvider)
        {
            this.Initialize(serviceProvider);

            this.OnViewLoaded();
        }

        public virtual void Unload()
        {
            if (this.Lifetime == LifetimeMode.KeepAlive)
            {
                this.Hide();
                this.SendToBack();
            }
            else
            {
                this.mdiUIContainer.Controls.Remove(this);
            }

            this.UnregisterPluginControllersEvents();

            this.OnViewUnloaded();
        }

        #endregion

        #region IViewService members

        public IPluginController<TPluginItem> CurrentPluginController
        {
            get 
            { 
                return this.currentModulePluginController; 
            }
        }

        public TPluginItem CurrentPluginItem
        {
            get 
            {
                return this.currentAppModulePluginItem; 
            }
        }

        public IPluginManager<TPluginItem> PluginManager
        {
            get 
            { 
                return this.modulePluginManager; 
            }
        }

        public Form CoreForm
        {
            get
            { 
                return this.coreForm; 
            }
        }

        public Control MdiUIContainer
        {
            get 
            { 
                return this.mdiUIContainer; 
            }
        }

        #endregion

        #region UI refresh, IView members

        public virtual void RefreshView()
        {
            // To Execute Common UI logic i.e. Register modeul plugin controllers events
            this.RegisterPluginControllersEvents();

            this.Show();
        }

        #endregion

        #region Protected methods

        protected virtual void Initialize(IPluginServiceProvider serviceProvider)
        {
            this.coreForm = serviceProvider.GetService(typeof(Form)) as Form;

            if (serviceProvider.Contains(typeof(IContainerControl)))
            {
                this.mdiUIContainer = serviceProvider.GetService(typeof(IContainerControl)) as Control;
            }

            this.currentModulePluginController = serviceProvider.GetService(typeof(IPluginController<>)) as IPluginController<TPluginItem>;

            this.modulePluginManager = serviceProvider.GetService(typeof(IPluginManager<>)) as IPluginManager<TPluginItem>;

            this.currentAppModulePluginItem = this.currentModulePluginController.PluginItem;

            if (this.mdiUIContainer == null)
            {
                throw new InfrastructureException("The container which is used to load this UI screen cannot be null. Please specify a container which inherits control. E.g. panel or form.");
            }

            if (this.mdiUIContainer != null &&
                !this.mdiUIContainer.Controls.Contains(this))
            {
                this.mdiUIContainer.SuspendLayout();

                this.Dock = DockStyle.Fill;

                this.mdiUIContainer.Controls.Add(this);
                this.mdiUIContainer.ResumeLayout();
            }

            this.Focus();

            this.Hide();
        }

        protected virtual void RegisterPluginControllersEvents() { }

        protected virtual void UnregisterPluginControllersEvents() { }

        protected virtual void OnViewLoaded()
        {
            if (this.Loaded != null)
            {
                this.Loaded(this, new PluginLoadedEventArgs(null, false, null));
            }
        }

        protected virtual void OnViewUnloaded()
        {
            if (this.Unloaded != null)
            {
                this.Unloaded(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
