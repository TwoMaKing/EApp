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
    public partial class UCCostPurchase : UCCostBase<PurchaseCostLineItem>
    {
        public UCCostPurchase()
        {
            InitializeComponent();
        }

        public override CostLineType CostType
        {
            get
            {
                return CostLineType.Purchase;
            }
        }

        protected override void RefreshViewCore()
        {
            base.RefreshViewCore();
        }

        protected override void InitializeCostColumns()
        {
            DataGridViewTextBoxColumn productionLifetimeColumn = new DataGridViewTextBoxColumn();
            productionLifetimeColumn.Name = CostColumnContainer.CostColumn_ProdLifeORLeaseTerm;
            productionLifetimeColumn.HeaderText = "Prod Lifetime";

            this.dgvCostLine.Columns.Add(productionLifetimeColumn);
        }
    }
}
