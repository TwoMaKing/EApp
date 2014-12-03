using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Infrastructure.DomainEvent
{
    public class DomainEventHandlerFactory
    {
        public static void Subscribe<TDomainEvent>(IEnumerable<IDomainEventHandler<TDomainEvent>> domainEventHandlers) where TDomainEvent : IDomainEvent 
        { 
        
        }


        public static void Publish<TDomainEvent>(TDomainEvent domainEvent) 
        { 
        
        }

    }
}
