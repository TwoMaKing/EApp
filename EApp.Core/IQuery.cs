using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.QueryPaging;

namespace EApp.Core
{
    public interface IQuery<TEntity, TIdentityKey> where TEntity : class, IEntity<TIdentityKey>
    {
        IList<TEntity> ToList(IEnumerable<TEntity> querySource);

        IPagingResult<TEntity> ToPagedList(IEnumerable<TEntity> querySource, int? pageNumber, int? pageSize);
    }

    public interface IQuery<TEntity> : IQuery<TEntity, int> where TEntity : class, IEntity
    { 
    
    }

}
