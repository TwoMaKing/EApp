using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public class StandardServiceCostLineItem : CostLineItemBase
    {
        public StandardServiceCostLineItem() : base() { }

        public override CostLineType Type
        {
            get 
            { 
                return CostLineType.StandardService; 
            }
        }

        [DefaultValue(InvoicingTreatment.Upfront)]
        public InvoicingTreatment InvoicingTreatment { get; set; }

    }
}
