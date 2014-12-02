using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public class LeaseCostLineItem : CostLineItemBase
    {
        public LeaseCostLineItem() : base() { }

        public override CostLineType Type
        {
            get 
            { 
                return CostLineType.Lease; 
            }
        }

        public decimal LeaseRateFactor { get; set; }

        [DefaultValue(CompliantLeaseOption.PartialAssetRecovery)]
        public CompliantLeaseOption CompliantLeaseOption { get; set; }
    }
}
