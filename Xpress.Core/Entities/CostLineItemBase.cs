using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EApp.DomainDriven.Infrastructure.Domain;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public abstract class CostLineItemBase : IEntity<Guid>
    {
        private Guid key;

        public CostLineItemBase() 
        {
            this.key = Guid.NewGuid();
        }

        public int Id { get; set; }

        public int LastModifiedUserId { get; set; }

        public DateTime LastModifiedDateTime { get; set; }

        public bool Checked { get; set; }

        [DefaultValue(ChangeStatus.New)]
        public ChangeStatus ChangeStauts { get; set; }

        public abstract CostLineType Type { get; }

        public int GroupId { get; set; }

        public int SubgroupId { get; set; }

        public ProductionSource ProductionSource { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalPrice { get; set; }

        public Group Group 
        {
            get 
            {
                return XpressTestingFakeData.Groups.SingleOrDefault(g => g.Id == this.GroupId); 
            } 
        }

        public Subgroup Subgroup 
        {
            get
            {
                return Group.Subgroups.SingleOrDefault(sg => sg.Id == SubgroupId);
            }
        }

        public User LastModifiedUser 
        {
            get 
            {
                return null;
            }
        }

        public Guid Key
        {
            get 
            {
                return this.key;
            }
        }
    }
}
