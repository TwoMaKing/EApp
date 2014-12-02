using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Core.Exceptions;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;

namespace EApp.Plugin.Generic.RibbonStyle
{
    public partial class UCRibbonPluginViewBase : UCPluginViewBase<RibbonModulePluginItem>
    {
        public UCRibbonPluginViewBase()
        {
            InitializeComponent();
        }

        protected Ribbon RibbonMenu
        {
            get;
            set;
        }

        protected override void Initialize(IPluginServiceProvider serviceProvider)
        {
            if (serviceProvider.Contains(typeof(Ribbon)))
            {
                this.RibbonMenu = serviceProvider.GetService(typeof(Ribbon)) as Ribbon;

                if (this.RibbonMenu == null)
                {
                    throw new InfrastructureException("The service provide doesn't contain Ribbon control. Please specify a Ribbon control.");
                }
            }

            base.Initialize(serviceProvider);
        }

        public sealed override void RefreshView()
        {
            // To Validate Privilege

            //Core logic to load data and firstly refresh UI when loading the screen.
            this.RefreshViewCore();

            // After completing UI logic, execute base.RefreshView() to show the screen. 
            base.RefreshView();
        }

        public override void Unload()
        {
            base.Unload();
        }

        protected virtual void RefreshViewCore() { }
    }
}
