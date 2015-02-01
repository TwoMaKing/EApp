using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Transactions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class DistributedTransactionAttribute : Attribute
    {
        
    }
}
