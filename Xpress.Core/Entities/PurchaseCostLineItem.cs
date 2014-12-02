using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public class PurchaseCostLineItem : CostLineItemBase
    {
        public PurchaseCostLineItem() : base() { }

        public override CostLineType Type
        {
            get 
            {
                return CostLineType.Purchase;
            }
        }

        [DefaultValue(10)]
        public int ProductionLifetime { get; set; }
    }
}
