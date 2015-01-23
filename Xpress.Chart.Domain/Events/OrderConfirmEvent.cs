using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Domain.Events;

namespace Xpress.Chat.Domain.Events
{
    public class OrderConfirmEvent : DomainEvent
    {
        public OrderConfirmEvent(IEntity source) : base(source) { }
    }
}
