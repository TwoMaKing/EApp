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
    public partial class UCCostLabor : UCCostBase<LaborCostLineItem>
    {
        public UCCostLabor()
        {
            InitializeComponent();
        }

        public override CostLineType CostType
        {
            get
            {
                return CostLineType.Labor;
            }
        }

        protected override void InitializeCostColumns()
        {
            DataGridViewComboBoxColumn jobTypeColumn = new DataGridViewComboBoxColumn();
            jobTypeColumn.Name = CostColumnContainer.CostColumn_JobType;
            jobTypeColumn.HeaderText = "Job Type";

            this.dgvCostLine.Columns.AddRange(new DataGridViewColumn[] { jobTypeColumn });

        }
    }
}
