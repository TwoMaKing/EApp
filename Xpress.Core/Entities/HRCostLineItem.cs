using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public class HRCostLineItem : CostLineItemBase
    {
        public HRCostLineItem() : base() { }

        public override CostLineType Type
        {
            get 
            { 
                return CostLineType.HR; 
            }
        }

        [DefaultValue(HRSettlementMode.RetentionBonus)]
        public HRSettlementMode SettlementMode { get; set; }
    }
}
