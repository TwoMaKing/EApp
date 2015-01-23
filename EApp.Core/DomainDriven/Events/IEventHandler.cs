using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Events
{
    /// <summary>
    /// Event handler. Handle an event by the specified event data.
    /// </summary>
    public interface IEventHandler<TEvent> : IHandler<TEvent> where TEvent : class, IEvent
    {

    }
}
