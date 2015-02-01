using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.DirectBus
{
    public class DirectBus : IBus
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

        public bool DistributedTransactionSupported
        {
            get { throw new NotImplementedException(); }
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
