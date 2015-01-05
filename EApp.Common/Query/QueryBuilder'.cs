using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;

namespace EApp.Common.Query
{
    public class QueryBuilder<TEntity> : QueryBuilder<TEntity, int>, IQueryBuilder<TEntity>
        where TEntity : class, IEntity
    {

    }
}
