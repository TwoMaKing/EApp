using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EApp.Core.DomainDriven.Domain.Events;
using EApp.Core.DomainDriven.Events;

namespace EApp.Core.DomainDriven.Domain
{
    public class AggregateRoot : EntityBase, IAggregateRoot
    {

        protected void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : class, IEvent
        {
            IEnumerable<IEventHandler<TDomainEvent>> eventHandlers = ServiceLocator.Instance.ResolveAll<IEventHandler<TDomainEvent>>();

            foreach (IEventHandler<TDomainEvent> eventHandler in eventHandlers)
            {
                if (eventHandler.GetType().IsDefined(typeof(HandleAsynchronizationAttribute), false))
                {
                    Task.Factory.StartNew(() => eventHandler.Handle(domainEvent));
                }
                else
                {
                    eventHandler.Handle(domainEvent);
                }
            }
        }

    }
}
