using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Transactions
{
    public abstract class TransactionCoordinator : ITransactionCoordinator
    {
        public virtual bool DistributedTransactionSupported
        {
            get 
            { 
                return true; 
            }
        }

        public bool Committed
        {
            get;
            protected set;
        }

        public abstract void Commit();

        public abstract void Rollback();

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected abstract void Dispose(bool disposing);
    }
}
