using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Infrastructure.Events.Bus
{
    public class EventBus : IEventBus
    {
        private Guid id = Guid.NewGuid();

        private IEventAggregator eventAggregator;

        public EventBus(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public Guid ID
        {
            get 
            {
                return this.id;
            }
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class, IEvent
        {
            this.eventAggregator.Publish<TMessage>(message);
        }

        public void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IEvent
        {
            foreach (TMessage message in messages)
            {
                this.eventAggregator.Publish<TMessage>(message);
            }
        }

        public void Clear()
        {
            
        }
    }
}
