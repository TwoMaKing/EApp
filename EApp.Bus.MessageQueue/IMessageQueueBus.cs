using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Compression;
using EApp.Core;

namespace EApp.Bus.MessageQueue
{
    public interface IMessageQueueBus<TMessage> : IUnitOfWork, IDisposable where TMessage : class
    {
        void Publish(TMessage message);

        void Publish(IEnumerable<TMessage> messages);
    }
}
