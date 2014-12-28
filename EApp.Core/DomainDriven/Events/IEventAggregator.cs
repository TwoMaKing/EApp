using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Events
{
    /// <summary>
    /// Event Aggregator used for Subscribe/Unsubscribe/Pulish event.
    /// </summary>
    public interface IEventAggregator
    {
        void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) 
            where TEvent : IEvent;

        void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) 
            where TEvent : IEvent;

        void Subscribe<TEvent>(Action<TEvent> eventHandlerAction) 
            where TEvent : IEvent;

        void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler) 
            where TEvent : IEvent;

        void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) 
            where TEvent : IEvent;

        void Unsubscribe<TEvent>(Action<TEvent> eventHandlerAction) 
            where TEvent : IEvent;

        void UnsubscribeAll<TEvent>() 
            where TEvent : IEvent;

        void UnsubscribeAll();

        IEnumerable<IEventHandler<TEvent>> GetSubscribedEventHandlers<TEvent>() 
            where TEvent : IEvent;

        void Publish<TEvent>(TEvent t) where TEvent : IEvent;

        void Publish<TEvent>(TEvent t, Action<TEvent, bool, Exception> callback, TimeSpan? timeout);
    }
}
