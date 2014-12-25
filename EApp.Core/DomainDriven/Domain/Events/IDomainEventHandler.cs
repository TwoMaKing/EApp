using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Events;

namespace EApp.Core.DomainDriven.Domain.Events
{
    public interface IDomainEventHandler<TDomainEvent> : IEventHandler<TDomainEvent> where TDomainEvent : class, IDomainEvent 
    { 
        
    }
}
