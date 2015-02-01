using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.Events
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
