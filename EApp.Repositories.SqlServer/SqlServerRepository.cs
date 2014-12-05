using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.Repository;

namespace EApp.Repositories.SqlServer
{
    public abstract class SqlServerRepository<TEntity> : RepositoryBase<TEntity>, IUnitOfWorkRepository 
        where TEntity : class, IEntity
    {
        public SqlServerRepository(IRepositoryContext repositoryContext) : base(repositoryContext) { }

        protected override void DoAdd(TEntity item)
        {
            this.RepositoryContext.RegisterAddedEntity(item);
        }

        protected override void DoAdd(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity entityItem in items)
            {
                this.RepositoryContext.RegisterAddedEntity(entityItem);
            }
        }

        protected override void DoUpdate(TEntity item)
        {
            this.RepositoryContext.RegisterModifiedEntity(item);
        }

        protected override void DoUpdate(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity entityItem in items)
            {
                this.RepositoryContext.RegisterModifiedEntity(entityItem);
            }
        }

        protected override void DoDelete(TEntity item)
        {
            this.RepositoryContext.RegisterDeletedEntity(item);
        }

        protected override void DoDelete(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity entityItem in items)
            {
                this.RepositoryContext.RegisterDeletedEntity(entityItem);
            }
        }

        protected override void DoDeleteByKey(Guid idOrKey)
        {
            TEntity itemToBeDeleted = this.DoFindByKey(idOrKey);

            this.DoDelete(itemToBeDeleted);
        }

        #region IUnitOfWorkRepository members

        public void PersistAddedItem(IEntity entity) 
        {
            this.PersistAddedItem((TEntity)entity);
        }

        public void PersistModifiedItem(IEntity entity) 
        {
            this.PersistModifiedItem((TEntity)entity);
        }

        public void PersistDeletedItem(IEntity entity) 
        {
            this.PersistDeletedItem((TEntity)entity);
        }

        #endregion

        #region Using SQL Script to persist Entity 

        protected abstract void PersistAddedItem(TEntity entity);

        protected abstract void PersistModifiedItem(TEntity entity);

        protected abstract void PersistDeletedItem(TEntity entity);

        #endregion

    }
}
