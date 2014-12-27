using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EApp.Core.DomainDriven.Events
{
    public class EventAggregator : IEventAggregator
    {
        private static object lockObject = new object();

        private Dictionary<Type, List<object>> eventHandlerList = new Dictionary<Type, List<object>>();

        private Func<object, object, bool> eventHandlerEquals = (object1, object2) =>
        {
            var o1Type = object1.GetType();
            var o2Type = object2.GetType();
            
            if (o1Type.IsGenericType &&
                o1Type.GetGenericTypeDefinition() == typeof(ActionDelegateEventHandler<>) &&
                o2Type.IsGenericType &&
                o2Type.GetGenericTypeDefinition() == typeof(ActionDelegateEventHandler<>))
            {
                return object1.Equals(object2);
            }

            return o1Type == o2Type;
        };


        public EventAggregator() 
        { 
        }

        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            Type eventType = typeof(TEvent);

            lock (lockObject)
            {
                if (!this.eventHandlerList.ContainsKey(eventType))
                {
                    this.eventHandlerList.Add(eventType, new List<object>());
                }

                var handlers = this.eventHandlerList[eventType];

                if (handlers == null)
                {
                    handlers = new List<object>();
                }

                if (!handlers.Exists(item => this.eventHandlerEquals(item, eventHandler)))
                {
                    handlers.Add(eventHandler);
                }
            }
        }

        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) where TEvent : IEvent
        {
            if (eventHandlers == null)
            {
                return;
            }

            foreach (IEventHandler<TEvent> eventHandlerItem in eventHandlers)
            {
                this.Subscribe<TEvent>(eventHandlerItem);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> eventHandlerAction) where TEvent : IEvent
        {
            this.Subscribe<TEvent>(new ActionDelegateEventHandler<TEvent>(eventHandlerAction));
        }

        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            Type eventType = typeof(TEvent);

            lock (lockObject)
            {
                if (!this.eventHandlerList.ContainsKey(eventType))
                {
                    return;
                }

                var handlers = this.eventHandlerList[eventType];

                if (handlers != null &&
                    handlers.Exists(item => this.eventHandlerEquals(item, eventHandler)))
                {
                    var eventHandlerToRemove = handlers.First(item => this.eventHandlerEquals(item, eventHandler));

                    handlers.Remove(eventHandlerToRemove);
                }
            }
        }

        public void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) where TEvent : IEvent
        {
            if (eventHandlers == null)
            {
                return;
            }

            foreach (IEventHandler<TEvent> eventHandlerItem in eventHandlers)
            {
                this.Unsubscribe<TEvent>(eventHandlerItem);
            }
        }

        public void Unsubscribe<TEvent>(Action<TEvent> eventHandlerAction) where TEvent : IEvent
        {
            this.Unsubscribe<TEvent>(new ActionDelegateEventHandler<TEvent>(eventHandlerAction));
        }

        public void UnsubscribeAll<TEvent>() where TEvent : IEvent
        {
            Type eventType = typeof(TEvent);

            lock (lockObject)
            {
                if (this.eventHandlerList != null &&
                    this.eventHandlerList.ContainsKey(eventType) &&
                    this.eventHandlerList[eventType] != null)
                {
                    this.eventHandlerList[eventType].Clear();
                }
            }
        }

        public void UnsubscribeAll()
        {
            lock (lockObject)
            {
                if (this.eventHandlerList != null)
                {
                    this.eventHandlerList.Clear();
                }
            }
        }

        public IEnumerable<IEventHandler<TEvent>> GetSubscribedEventHandlers<TEvent>() where TEvent : IEvent
        {
            Type eventType = typeof(TEvent);
         
            if (this.eventHandlerList == null ||
                !this.eventHandlerList.ContainsKey(eventType) ||
                this.eventHandlerList[eventType] == null ||
                this.eventHandlerList[eventType].Count.Equals(0))
            {
                return null;
            }

            var handlers = this.eventHandlerList[eventType];

            return handlers.Select(item => item as IEventHandler<TEvent>).ToList();
        }

        public void Publish<TEvent>(TEvent t) where TEvent : IEvent
        {
            Type eventType = typeof(TEvent);

            if (this.eventHandlerList != null &&
                this.eventHandlerList.ContainsKey(eventType) &&
                this.eventHandlerList[eventType] != null &&
                this.eventHandlerList[eventType].Count > 0)
            {
                List<object> handlers = this.eventHandlerList[eventType];

                foreach (object handlerObject in handlers)
                {
                    if (!(handlerObject is IEventHandler<TEvent>))
                    {
                        continue;
                    }

                    IEventHandler<TEvent> eventHandler = handlerObject as IEventHandler<TEvent>;
                    // Async Operation
                    Task.Factory.StartNew((o) => eventHandler.Handle((TEvent)o), t);
                }
            }
        }
    }
}
