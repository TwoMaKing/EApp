using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public interface IDbContext : IDisposable
    {
        DbConfiguration DbConfiguration { get; }

        Database Database { get; }

        IDbSet<TEntityType> Set<TEntityType>();

        IDbSet Set(Type entityType);
    }
}
