using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.UnitOfWork;

namespace EApp.Infrastructure.Repository
{

    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository<TAggregateRoot, TKey> where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        /// <summary>
        /// Return a Unit Of Work
        /// </summary>
        IRepositoryContext RepositoryContext { get; }

        /// <summary>
        ///  Aadd a item to Repository
        /// </summary>
        void Add(TAggregateRoot item);

        /// <summary>
        /// Add items to Repository
        /// </summary>
        void Add(IEnumerable<TAggregateRoot> items);

        /// <summary>
        /// Update a item to Repository
        /// </summary>
        void Update(TAggregateRoot item);

        /// <summary>
        /// Update items to Repository
        /// </summary>
        void Update(IEnumerable<TAggregateRoot> items);

        /// <summary>
        /// Delete the specified item from Repository
        /// </summary>
        void Delete(TAggregateRoot item);

        /// <summary>
        /// Delete a item from Repository by item key
        /// </summary>
        void Delete(TKey idOrKey);

        /// <summary>
        /// Delete specified items from Repository
        /// </summary>
        void Delete(IEnumerable<TAggregateRoot> items);

        /// <summary>
        /// Find the specific aggregate root by id or key
        /// </summary>
        TAggregateRoot FindByKey(TKey idOrKey);

        /// <summary>
        /// Find the specific aggregate root by the specification from repository.
        /// </summary>
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// Find all of aggregate roots from repository.
        /// </summary>
        /// <returns></returns>
        IQueryable<TAggregateRoot> FindAll();

        /// <summary>
        /// Find all of aggregate roots matching query expression from repository.
        /// </summary>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression);

        /// <summary>
        /// Find all of aggregate roots matching query expression from repository.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression, int pageNumber, int pageSize);

        /// <summary>
        /// Find all of aggregate roots by the specification from repository,
        /// </summary>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification);


        /// <summary>
        /// Find all of specific Aggregate Root by the specification from repository
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, int pageNumber, int pageSize);
    }

    public interface IRepository<TAggregateRoot> : IRepository<TAggregateRoot, Guid>
        where TAggregateRoot : class, IAggregateRoot<Guid> , IAggregateRoot
    { 
        
    }
}
