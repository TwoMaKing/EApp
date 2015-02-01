using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core
{
    /// <summary>
    /// The provider source of handlers. e.g handlers configured in the configuration file/xml file or hard codes.
    /// </summary>
    public interface IHandlerProvider
    {
        /// <summary>
        /// Return the dictionary of handler (e.g. domain event handler or CQRS command handler)
        /// Key is the type of message e.g. event type or command type.
        /// Value is the type of handler. e.g. event handler type or command handler type.
        /// </summary>
        IDictionary<Type, Type> GetHandlers();
    }
}
