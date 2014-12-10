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
    public interface IRepositoryContext: IUnitOfWork
    {
        Guid Id { get; }

        void RegisterAdded(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository);

        void RegisterModified(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository);

        void RegisterDeleted(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository);
    }

}
