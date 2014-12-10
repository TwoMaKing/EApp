using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Repository;
using EApp.Infrastructure.UnitOfWork;

namespace EApp.Repositories.SqlServer
{
    public interface ISqlServerRepositoryContext : IRepositoryContext
    {
        DbTransaction Transaction { get; }
    }
}
