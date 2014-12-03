using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;

namespace EApp.Infrastructure.Repository
{
    /// <summary>
    /// Used for pure SQL Scripts for Creation/Update/Deletion i.e. Non-ORMapping tool
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
