using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;

namespace EApp.Core.DomainDriven.Repository
{
    /// <summary>
    /// Actually used for Entity Creation/Update/Deletion i.e .pure SQL Scripts, Entity Framework, NH.
    /// </summary>
    public interface IUnitOfWorkRepository
    {
        /// <summary>
        /// Execute SQL for creating a new item.
        /// </summary>
        void PersistAddedItem(IEntity entity);

        /// <summary>
        /// Execute SQL for updating a item.
        /// </summary>
        void PersistModifiedItem(IEntity entity);

        /// <summary>
        /// Execute SQL for deleting a item.
        /// </summary>
        void PersistDeletedItem(IEntity entity);
    }
}
