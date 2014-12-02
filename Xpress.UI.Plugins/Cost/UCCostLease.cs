using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xpress.Core.Entities;
using Xpress.Core.Logic;
using Xpress.Core.Common;

namespace Xpress.UI.Plugins.Cost
{
    public partial class UCCostLease : UCCostBase<LeaseCostLineItem>
    {
        public UCCostLease()
        {
            InitializeComponent();
        }

        public override CostLineType CostType
        {
            get
            {
                return CostLineType.Lease;
            }
        }

        protected override void InitializeCostColumns()
        {
            DataGridViewTextBoxColumn leaseRateFactorColumn = new DataGridViewTextBoxColumn();
            leaseRateFactorColumn.Name = CostColumnContainer.CostColumn_LeaseRateFactor;
            leaseRateFactorColumn.HeaderText = "Lease Rate Factor";

            DataGridViewComboBoxColumn compliantLeaseOptionColumn = new DataGridViewComboBoxColumn();
            compliantLeaseOptionColumn.Name = CostColumnContainer.CostColumn_CompliantLeaseOption;
            compliantLeaseOptionColumn.HeaderText = "Compliant Lease Option";

            this.dgvCostLine.Columns.AddRange(new DataGridViewColumn[] { leaseRateFactorColumn, compliantLeaseOptionColumn });
        }

    }
}
