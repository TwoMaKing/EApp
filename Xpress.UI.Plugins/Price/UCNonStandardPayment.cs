using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Common.Util;
using EApp.Core.Application;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using EApp.Plugin.Generic.RibbonStyle;
using EApp.UI.Controls.GridView;
using EApp.UI.Controls.UIHandler;
using Xpress.Core.Common;
using Xpress.Core.Entities;
using Xpress.Core.Logic;
using Xpress.Core.Plugin;

namespace Xpress.UI.Plugins.Price
{
    public partial class UCNonStandardPayment : UCRibbonPluginViewBase
    {
        public UCNonStandardPayment()
        {
            InitializeComponent();
        }

        protected override void RegisterPluginControllersEvents()
        {
            this.PluginManager.PluginControllers["Add Non-Standard Payment"].Loading += new CancelEventHandler(this.OnAddNewNonStandardPayementLoading);
            this.PluginManager.PluginControllers["Add Non-Standard Payment"].Loaded += new EventHandler<PluginLoadedEventArgs>(this.OnAddNewNonStandardPayementLoaded);

            base.RegisterPluginControllersEvents();
        }

        protected override void UnregisterPluginControllersEvents()
        {
            this.PluginManager.PluginControllers["Add Non-Standard Payment"].Loading -= new CancelEventHandler(this.OnAddNewNonStandardPayementLoading);
            this.PluginManager.PluginControllers["Add Non-Standard Payment"].Loaded -= new EventHandler<PluginLoadedEventArgs>(this.OnAddNewNonStandardPayementLoaded);

            base.UnregisterPluginControllersEvents();
        }

        private void OnAddNewNonStandardPayementLoading(object sender, CancelEventArgs e) 
        {
            if (!string.IsNullOrEmpty(txtName.Text) ||
                !string.IsNullOrEmpty(txtID.Text) ||
                !string.IsNullOrEmpty(txtDesc.Text))
            {
                DialogResult ret = MessageBox.Show("", "", MessageBoxButtons.YesNoCancel);

                if (ret == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (ret == DialogResult.Yes)
                { 
                    // Save the current edited data.
                }

                return;
            }
        }

        private void OnAddNewNonStandardPayementLoaded(object sender,PluginLoadedEventArgs e)
        {
            lvwPayments.Items.Add(txtName.Text);
        }

    }
}
