using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.UnitOfWork;

namespace EApp.Infrastructure.Repository
{
    /// <summary>
    /// The implemented classes are repository transaction contexts
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepositoryContext<TIdentityKey> : IUnitOfWork
    {
        TIdentityKey Id { get; }

        void RegisterAddedEntity(IEntity<TIdentityKey> entity);

        void RegisterModifiedEntity(IEntity<TIdentityKey> entity);

        void RegisterDeletedEntity(IEntity<TIdentityKey> entity);
    }

    public interface IRepositoryContext : IRepositoryContext<Guid>
    { 
        
    }
}
