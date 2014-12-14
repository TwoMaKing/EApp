using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Events;
using EApp.Infrastructure.Events.Bus;

namespace EApp.Infrastructure.Domain.Events
{
    public class DomainEventAggregator : EventAggregator
    {
        private readonly static DomainEventAggregator instance = new DomainEventAggregator();

        public DomainEventAggregator Instance 
        {
            get 
            {
                return instance;
            }
        }
    }
}
