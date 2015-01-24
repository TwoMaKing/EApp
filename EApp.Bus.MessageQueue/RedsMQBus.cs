using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Bus;

namespace EApp.Bus.MessageQueue
{
    public class RedsMQBus : IBus
    {
        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

        public void Publish<TMessage>(TMessage message)
        {
            throw new NotImplementedException();
        }

        public void Publish<TMessage>(IEnumerable<TMessage> messages)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Committed
        {
            get { throw new NotImplementedException(); }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
