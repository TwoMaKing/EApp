using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.Query;

namespace EApp.Data.Query
{
    public interface ISqlQueryBuilder<TEntity, TIdentityKey> : IQuery<TEntity, TIdentityKey> where TEntity : class, IEntity<TIdentityKey>
    {
        ISqlQueryBuilder<TEntity, TIdentityKey> From(string table);

        ISqlQueryBuilder<TEntity, TIdentityKey> Where(string wherePredicate, object[] parameters);

        ISqlQueryBuilder<TEntity, TIdentityKey> Select(string[] columns);

        ISqlQueryBuilder<TEntity, TIdentityKey> OrderBy(string column, SortOrder sortOrder);
    }

    public interface ISqlQueryBuilder<TEntity> : ISqlQueryBuilder<TEntity, int> where TEntity : class, IEntity 
    { 
    
    }
}
