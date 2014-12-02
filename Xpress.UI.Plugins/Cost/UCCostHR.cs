using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xpress.Core.Common;
using Xpress.Core.Entities;

namespace Xpress.UI.Plugins.Cost
{
    public partial class UCCostHR : UCCostBase<HRCostLineItem>
    {
        public UCCostHR()
        {
            InitializeComponent();
        }

        public override CostLineType CostType
        {
            get
            {
                return CostLineType.HR;
            }
        }

        protected override void InitializeCostColumns()
        {
            DataGridViewComboBoxColumn hrSettlementColumn = new DataGridViewComboBoxColumn();
            hrSettlementColumn.Name = CostColumnContainer.CostColumn_HRSettlement;
            hrSettlementColumn.HeaderText = "HR Item Type";

            this.dgvCostLine.Columns.AddRange(new DataGridViewColumn[] { hrSettlementColumn });
        }
    }
}
