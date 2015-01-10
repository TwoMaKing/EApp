using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;

namespace EApp.Data.Query
{
    public class SqlQueryBuilder<TEntity, TIdentity> : ISqlQueryBuilder<TEntity, TIdentity> where TEntity : class, IEntity<TIdentity>
    {

        public ISqlQueryBuilder<TEntity, TIdentity> From(string table)
        {
            throw new NotImplementedException();
        }

        public ISqlQueryBuilder<TEntity, TIdentity> Where(string wherePredicate, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public ISqlQueryBuilder<TEntity, TIdentity> Select(string[] columns)
        {
            throw new NotImplementedException();
        }

        public ISqlQueryBuilder<TEntity, TIdentity> OrderBy(string column, Core.Query.SortOrder sortOrder)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> ToList(IEnumerable<TEntity> querySource)
        {
            throw new NotImplementedException();
        }

        public Core.Query.IPagingResult<TEntity> ToPagedList(IEnumerable<TEntity> querySource, int? pageNumber, int? pageSize)
        {
            throw new NotImplementedException();
        }

    }
}
