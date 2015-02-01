using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.Events
{
    /// <summary>
    /// Event Aggregator used for Subscribe/Unsubscribe/Pulish event.
    /// </summary>
    public interface IEventAggregator
    {
        void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) 
            where TEvent : class, IEvent;

        void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent;

        void Subscribe<TEvent>(Action<TEvent> eventHandlerAction)
            where TEvent : class, IEvent;

        void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent;

        void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent;

        void Unsubscribe<TEvent>(Action<TEvent> eventHandlerAction)
            where TEvent : class, IEvent;

        void UnsubscribeAll<TEvent>()
            where TEvent : class, IEvent;

        void UnsubscribeAll();

        IEnumerable<IEventHandler<TEvent>> GetSubscribedEventHandlers<TEvent>()
            where TEvent : class, IEvent;

        void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;

        void Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout) 
            where TEvent : class, IEvent;
    }
}
