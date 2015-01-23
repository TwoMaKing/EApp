using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Events;
using Xpress.Chat.Domain.Events;
using System.Threading.Tasks;

namespace Xpress.Chat.Domain.Models
{
    public class Order : AggregateRoot
    {
        public void Confirm() 
        {
            // confirmation logic

            OrderConfirmEvent @event = new OrderConfirmEvent(this);

            this.RaiseEvent<OrderConfirmEvent>(@event);
        }
    }
}
