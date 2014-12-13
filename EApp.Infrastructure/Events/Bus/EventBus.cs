using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Infrastructure.Events.Bus
{
    public class EventBus : IEventBus
    {
        public Guid ID
        {
            get { throw new NotImplementedException(); }
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class, IEvent
        {

        }

        public void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IEvent
        {

        }

        public void Clear()
        {

        }
    }
}
