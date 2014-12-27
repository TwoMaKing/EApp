using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Events;
using EApp.Core.Application;


namespace EApp.Core.DomainDriven.Domain.Events
{
    public class DomainEventAggregator : EventAggregator
    {
        private readonly static DomainEventAggregator instance = new DomainEventAggregator();

        public static DomainEventAggregator Instance 
        {
            get 
            {
                return instance;
            }
        }
    }
}
