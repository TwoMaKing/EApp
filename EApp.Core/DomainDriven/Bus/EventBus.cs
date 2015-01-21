using EApp.Core.DomainDriven.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace EApp.Core.DomainDriven.Bus
{
    /// <summary>
    /// Domain event buses. We can use event bus to publish domain event.
    /// </summary>
    public class EventBus : IEventBus
    {
        private ThreadLocal<Queue<IEvent>> eventQueue = new ThreadLocal<Queue<IEvent>>(() => new Queue<IEvent>());
        
        private ThreadLocal<bool> committed = new ThreadLocal<bool>(() => true);

        private Guid id = Guid.NewGuid();

        private IEventAggregator eventAggregator;

        private MethodInfo publishMethod;

        public EventBus(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.publishMethod = (from m in this.eventAggregator.GetType().GetMethods()
                                  let parameters = m.GetParameters()
                                  let methodName = m.Name
                                  where methodName == "Publish" &&
                                  parameters != null &&
                                  parameters.Length == 1
                                  select m).First();
        }

        public Guid Id
        {
            get 
            {
                return this.id;
            }
        }

        public void Publish<TMessage>(TMessage message)
        {
            if (!(message is IEvent))
            {
                throw new ArgumentException("The Message must be a instance of event that implements IEvent.");
            }

            this.eventQueue.Value.Enqueue((IEvent)message);
        }

        public void Publish<TMessage>(IEnumerable<TMessage> messages)
        {
            foreach (TMessage message in messages)
            {
                this.Publish<TMessage>(message);
            }
        }

        public void Clear()
        {
            this.eventQueue.Value.Clear();
        }

        public bool Committed
        {
            get 
            { 
                return this.committed.Value; 
            }
        }

        public void Commit()
        {
            while (this.eventQueue.Value.Count > 0)
            {
                IEvent e = this.eventQueue.Value.Dequeue();
                Type eventType = e.GetType();

                MethodInfo method = this.publishMethod.MakeGenericMethod(eventType);
                method.Invoke(this.eventAggregator, new object[] { e });
            }

            this.committed.Value = true;
        }

        public void Rollback()
        {
            this.Clear();
        }

        public void Dispose()
        {
            this.Clear();
            this.eventQueue.Dispose();
        }
    }
}
