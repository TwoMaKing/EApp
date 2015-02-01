using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.Bus
{
    public class EventBus : IEventBus
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class, Events.IEvent
        {
            throw new NotImplementedException();
        }

        public void Publish<TEvent>(IEnumerable<TEvent> events) where TEvent : class, Events.IEvent
        {
            throw new NotImplementedException();
        }

        public bool DistributedTransactionSupported
        {
            get { throw new NotImplementedException(); }
        }

        public bool Committed
        {
            get { throw new NotImplementedException(); }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
