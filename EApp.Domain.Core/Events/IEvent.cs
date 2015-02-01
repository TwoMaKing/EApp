using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.Events
{
    /// <summary>
    /// Represent the class which implements the interface is a event data type.
    /// </summary>
    public interface IEvent : IEntity<int>, IEntity
    {
        /// <summary>
        /// The date time when generates this event. It can be UTC time.
        /// </summary>
        DateTime TimeStamp { get; }
    }
}
