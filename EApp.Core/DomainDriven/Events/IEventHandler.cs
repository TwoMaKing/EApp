using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Events
{
    /// <summary>
    /// Event handler.
    /// </summary>
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Handle a event by the specified event data.
        /// </summary>
        /// <param name="t">the event data instance which implements the IEvent interface.</param>
        void Handle(TEvent t);
    }
}
