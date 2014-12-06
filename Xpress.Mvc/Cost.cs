using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Core.Application;
using EApp.Core.Configuration;
using EApp.Windows.Mvc;
using Xpress.Mvc.Models;

namespace Xpress.Mvc
{
    public partial class Cost : FormViewBase
    {
        public Cost()
        {
            InitializeComponent();
        }

        public void BindCosts(CostModel costModel) 
        { 
            //Bind cost to data grid view.

            dataGridView1.DataSource = costModel.Costs;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.View.Action("AddCost");
        }
    }
}
