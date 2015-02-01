using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EApp.Domain.Core.Repositories;

namespace EApp.Repositories.SQL
{
    /// <summary>
    /// Repository Context for Sql Server.
    /// </summary>
    public interface ISQLRepositoryContext : IRepositoryContext
    {
        ISQLRepositoryContextDbProvider DbProvider { get; }
    }
}
