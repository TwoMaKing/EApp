using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.QueryPaging;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.UnitOfWork;

namespace EApp.Infrastructure.Repository
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository<TEntity, TIdentityKey> where TEntity : class, IEntity<TIdentityKey>
    {
        /// <summary>
        /// Return a Unit Of Work
        /// </summary>
        IRepositoryContext RepositoryContext { get; }

        /// <summary>
        ///  Aadd a item to Repository
        /// </summary>
        void Add(TEntity item);

        /// <summary>
        /// Add items to Repository
        /// </summary>
        void Add(IEnumerable<TEntity> items);

        /// <summary>
        /// Update a item to Repository
        /// </summary>
        void Update(TEntity item);

        /// <summary>
        /// Update items to Repository
        /// </summary>
        void Update(IEnumerable<TEntity> items);

        /// <summary>
        /// Delete the specified item from Repository
        /// </summary>
        void Delete(TEntity item);

        /// <summary>
        /// Delete a item from Repository by item key
        /// </summary>
        void Delete(TIdentityKey id);

        /// <summary>
        /// Delete specified items from Repository
        /// </summary>
        void Delete(IEnumerable<TEntity> items);

        /// <summary>
        /// Find the specific aggregate root by id or key
        /// </summary>
        TEntity FindByKey(TIdentityKey id);

        /// <summary>
        /// Find the specific aggregate root by the specification from repository.
        /// </summary>
        TEntity Find(ISpecification<TEntity> specification);

        /// <summary>
        /// Find all of aggregate roots from repository.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> FindAll();

        /// <summary>
        /// Find all of aggregate roots matching paging condition from repository.
        /// </summary>
        IPagingResult<TEntity> FindAll(int pageNumber, int pageSize);

        /// <summary>
        /// Find all of entites matching query expression from repository.
        /// </summary>
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Find all of entities matching query expression and paging condition from repository.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        IPagingResult<TEntity> FindAll(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize);

        /// <summary>
        /// Find all of aggregate roots by the specification from repository,
        /// </summary>
        IQueryable<TEntity> FindAll(ISpecification<TEntity> specification);

        /// <summary>
        /// Find all of specific Aggregate Root by the specification and paging condition from repository
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        IPagingResult<TEntity> FindAll(ISpecification<TEntity> specification, int pageNumber, int pageSize);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IEntity<int>, IEntity { }
}
