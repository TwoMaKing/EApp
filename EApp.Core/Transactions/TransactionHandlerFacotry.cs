using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Transactions
{
    public sealed class TransactionHandlerFacotry
    {
        public static ITransactionHandler Create() 
        {
            return new DistributedTransactionHandler();    
        }
    }
}
