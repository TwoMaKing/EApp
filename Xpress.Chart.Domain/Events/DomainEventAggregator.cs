using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Domain.Core.Events;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;
using Xpress.Chat.Domain.Repositories;
using Xpress.Chat.Domain.Services;

namespace Xpress.Chat.Domain.Events
{
    public class DomainEventAggregator : EventAggregator
    {
        private readonly static DomainEventAggregator instance = new DomainEventAggregator();

        public DomainEventAggregator()
        {
            IDomainEventHandler<PostDomainEvent>  postDomainEventHandler = 
                ServiceLocator.Instance.GetService<IDomainEventHandler<PostDomainEvent>>();

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
