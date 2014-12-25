using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Events;

namespace EApp.Core.DomainDriven.Domain.Events
{
    /// <summary>
    /// The domain event data.
    /// </summary>
    public interface IDomainEvent : IEvent
    {
        /// <summary>
        /// The source which generate this event.
        /// </summary>
        IEntity Source { get; }
    }
}
