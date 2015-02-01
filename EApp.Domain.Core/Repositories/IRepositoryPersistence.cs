using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.Repositories
{
    /// <summary>
    /// Actually used for Entity Creation/Update/Deletion i.e .pure SQL Scripts execution, Entity Framework, NH, MongoDB etc.
    /// </summary>
    public interface IRepositoryPersistence
    {
        /// <summary>
        /// Execute database persistence for creating new items.
        /// </summary>
        void PersistAddedItems(IEnumerable<object> objects);

        /// <summary>
        /// Execute database persistence for updating items.
        /// </summary>
        void PersistModifiedItems(IEnumerable<object> objects);

        /// <summary>
        /// Execute database persistence for deleting items.
        /// </summary>
        void PersistDeletedItems(IEnumerable<object> objects);
    }
}
