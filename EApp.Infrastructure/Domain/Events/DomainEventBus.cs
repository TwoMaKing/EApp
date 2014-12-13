using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Events;
using EApp.Infrastructure.Events.Bus;

namespace EApp.Infrastructure.Domain.Events
{
    public class DomainEventBus : EventBus
    {
        private readonly static DomainEventBus instance = new DomainEventBus();

        public DomainEventBus Instance 
        {
            get 
            {
                return instance;
            }
        }

    }
}
