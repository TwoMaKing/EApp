using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;

namespace EApp.Data.Query
{
    public class SqlQueryBuilder<TEntity> : SqlQueryBuilder<TEntity, int>, ISqlQueryBuilder<TEntity> where TEntity : class, IEntity
    {

    }
}
