using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Domain.Events;
using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Models;
using Xpress.Chart.Domain.Repositories;
using Xpress.Chart.Domain.Services;


namespace Xpress.Chart.Domain.Events
{
    public class DomainEventAggregator : EventAggregator
    {
        private readonly static DomainEventAggregator instance = new DomainEventAggregator();

        public DomainEventAggregator()
        { 
            IDomainEventHandler<PostDomainEvent> postDomainEventHandler = 
                EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve<IDomainEventHandler<PostDomainEvent>>();

            this.Subscribe<PostDomainEvent>(postDomainEventHandler);
        }

        public static DomainEventAggregator Instance 
        {
            get 
            {
                return instance;
            }
        }
    }
}
