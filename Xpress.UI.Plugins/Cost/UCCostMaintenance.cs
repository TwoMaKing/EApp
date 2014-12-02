using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xpress.Core.Common;
using Xpress.Core.Entities;

namespace Xpress.UI.Plugins.Cost
{
    public partial class UCCostMaintenance : UCCostBase<MaintenanceCostLineItem>
    {
        public UCCostMaintenance()
        {
            InitializeComponent();
        }

        public override CostLineType CostType
        {
            get
            {
                return CostLineType.Maintenance;
            }
        }

        protected override void InitializeCostColumns()
        {
            DataGridViewTextBoxColumn serviceAmountColumn = new DataGridViewTextBoxColumn();
            serviceAmountColumn.Name = CostColumnContainer.CostColumn_ServiceAmount;
            serviceAmountColumn.HeaderText = "Service Amount";

            this.dgvCostLine.Columns.AddRange(new DataGridViewColumn[] { serviceAmountColumn });
        }
    }
}
