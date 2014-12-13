using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Events;

namespace EApp.Infrastructure.Domain.Events
{
    public interface IDomainEventHandler<TDomainEvent> : IEventHandler<TDomainEvent> where TDomainEvent : class, IDomainEvent 
    { 
        
    }
}
