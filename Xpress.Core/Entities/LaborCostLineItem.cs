using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public class LaborCostLineItem : CostLineItemBase
    {
        public LaborCostLineItem() : base() { }

        public override CostLineType Type
        {
            get 
            {
                return CostLineType.Labor; 
            }
        }

        public int JobTypeId { get; set; }

        public JobType JobType
        {
            get 
            {
                return XpressTestingFakeData.JobTypes.SingleOrDefault(j => j.Id == JobTypeId);
            }
        }
    }
}
