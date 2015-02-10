using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;

namespace EApp.Domain.Core.Repositories
{
    /// <summary>
    /// The implemented classes are repository transaction contexts
    /// Multiple repositories are included into the same context.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepositoryContext: IUnitOfWork, IDisposable
    {
        Guid Id { get; }

        IRepository<TAggregateRoot> GetRepository<TAggregateRoot>()
            where TAggregateRoot : class, IAggregateRoot<int>, IAggregateRoot, new();

        void RegisterAdded(IEnumerable<object> objects, IRepositoryPersistence persistence);

        void RegisterModified(IEnumerable<object> objects, IRepositoryPersistence persistence);

        void RegisterDeleted(IEnumerable<object> objects, IRepositoryPersistence persistence);
    }

}
