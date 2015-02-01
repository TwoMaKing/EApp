using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EApp.Core;
using EApp.Domain.Core;
using Xpress.Chat.Domain.Events;

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
