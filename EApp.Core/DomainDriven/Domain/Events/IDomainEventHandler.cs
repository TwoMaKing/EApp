using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Events;

namespace EApp.Core.DomainDriven.Domain.Events
{
    /// <summary>
    /// Domain event handler. Handle an domain event by the specified domain event data.
    /// 1. Notification. e.g. send email/Send dispatch form to the different vendor/warehouse after confirming/paying a order. 
    /// 2. Handle some kinds of scattered business.
    /// 3. Decouple between domian model and repository/application service. Domain model is not allowed to access repository/application service.
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public interface IDomainEventHandler<TDomainEvent> : IEventHandler<TDomainEvent> 
        where TDomainEvent : class, IDomainEvent 
    { 
        
    }
}
