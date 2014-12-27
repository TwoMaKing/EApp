using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.UnitOfWork;

namespace EApp.Core.Transactions
{
    public interface ITransactionHandler : IUnitOfWork, IDisposable
    {

    }
}
