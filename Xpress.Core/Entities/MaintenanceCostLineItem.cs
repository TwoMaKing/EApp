using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public class MaintenanceCostLineItem : CostLineItemBase
    {
        public MaintenanceCostLineItem() : base() { }

        public override CostLineType Type
        {
            get 
            { 
                return CostLineType.Maintenance; 
            }
        }

        public decimal? ServiceAmount { get; set; }

    }
}
