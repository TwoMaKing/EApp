using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.UnitOfWork;


namespace EApp.Core.DomainDriven.Repository
{
    /// <summary>
    /// The implemented classes are repository transaction contexts
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepositoryContext: IUnitOfWork, IDisposable
    {
        Guid Id { get; }

        void RegisterAdded(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository);

        void RegisterModified(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository);

        void RegisterDeleted(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository);
    }

}
