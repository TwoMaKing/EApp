using EApp.Core.DomainDriven.Events;
using EApp.Core.DomainDriven.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Bus
{
    /// <summary>
    ///  The message bus.
    /// </summary>
    public interface IBus : IUnitOfWork, IDisposable
    {
        Guid Id { get; }
        /// <summary>
        /// Publishes the specified message to the bus.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        void Publish<TMessage>(TMessage message);
        /// <summary>
        /// Publishes a collection of messages to the bus.
        /// </summary>
        /// <param name="messages">The messages to be published.</param>
        void Publish<TMessage>(IEnumerable<TMessage> messages);
        /// <summary>
        /// Clears the published messages waiting for commit.
        /// </summary>
        void Clear();
    }
}
