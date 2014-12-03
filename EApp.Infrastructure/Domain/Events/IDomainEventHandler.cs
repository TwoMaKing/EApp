using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Infrastructure.DomainEvent
{
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void Handle(TDomainEvent domainEvent);
    }
}
