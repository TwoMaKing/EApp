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
    public partial class UCCostStandardService : UCCostBase<StandardServiceCostLineItem>
    {
        public UCCostStandardService()
        {
            InitializeComponent();
        }

        public override CostLineType CostType
        {
            get
            {
                return CostLineType.StandardService;
            }
        }

        protected override void InitializeCostColumns()
        {
            DataGridViewComboBoxColumn invTreatColumn = new DataGridViewComboBoxColumn();
            invTreatColumn.Name = CostColumnContainer.CostColumn_InvTreat;
            invTreatColumn.HeaderText = "Invoice Treatmeant";

            this.dgvCostLine.Columns.AddRange(new DataGridViewColumn[] { invTreatColumn });
        }
    }
}
