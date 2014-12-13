using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Infrastructure.Events
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
            lock (lockObject)
            {
                Type eventType = typeof(TEvent);

                if (this.eventHandlerList.ContainsKey(eventType))
                {
                    List<object> handlers = this.eventHandlerList[eventType];

                    if (handlers == null)
                    {
                        handlers = new List<object>();
                    }

                    if (!handlers.Exists(item => this.eventHandlerEquals(item, eventHandler)))
                    {
                        this.eventHandlerList[eventType].Add(eventHandler);
                    }
                }
                else
                {
                    this.eventHandlerList.Add(eventType, new List<object>() { eventHandler });
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
            throw new NotImplementedException();
        }

        public void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TEvent>(Action<TEvent> eventHandlerAction) where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeAll<TEvent>() where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEventHandler<TEvent>> GetSubscribedEventHandlers<TEvent>() where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void Publish<TEvent>(TEvent t) where TEvent : IEvent
        {
            throw new NotImplementedException();
        }
    }
}
