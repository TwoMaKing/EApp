using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;


namespace EApp.Core.Transactions
{
    public class DistributedTransactionCoordinator : TransactionCoordinator
    {
        private TransactionScope transactionScope;

        public DistributedTransactionCoordinator() : this(IsolationLevel.ReadCommitted) { }

        public DistributedTransactionCoordinator(IsolationLevel isolationLevel)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = isolationLevel;

            this.transactionScope = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public override void Commit()
        {
            transactionScope.Complete();
        }

        public override void Rollback()
        {
            return;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                transactionScope.Dispose();
            }
        }
    }
}
