using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.Events;

namespace EApp.Infrastructure.Domain.Events
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
