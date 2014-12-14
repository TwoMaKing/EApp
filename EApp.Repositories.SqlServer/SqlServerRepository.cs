using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.Repository;

namespace EApp.Repositories.SqlServer
{
    public abstract class SqlServerRepository<TEntity> : Repository<TEntity>, IUnitOfWorkRepository 
        where TEntity : class, IEntity
    {
        private ISqlServerRepositoryContext sqlServerRepositoryContext;

        public SqlServerRepository(ISqlServerRepositoryContext repositoryContext) : base(repositoryContext) 
        {
            sqlServerRepositoryContext = this.RepositoryContext as ISqlServerRepositoryContext;
        }

        protected DbTransaction SqlServerTranscation
        {
            get 
            {
                return this.sqlServerRepositoryContext.Transaction;
            }
        }

        protected override void DoAdd(TEntity item)
        {
            this.RepositoryContext.RegisterAdded(item, this);
        }

        protected override void DoAdd(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity entityItem in items)
            {
                this.RepositoryContext.RegisterAdded(entityItem, this);
            }
        }

        protected override void DoUpdate(TEntity item)
        {
            this.RepositoryContext.RegisterModified(item, this);
        }

        protected override void DoUpdate(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity entityItem in items)
            {
                this.RepositoryContext.RegisterModified(entityItem, this);
            }
        }

        protected override void DoDelete(TEntity item)
        {
            this.RepositoryContext.RegisterDeleted(item, this);
        }

        protected override void DoDelete(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity entityItem in items)
            {
                this.RepositoryContext.RegisterDeleted(entityItem, this);
            }
        }

        protected override void DoDeleteByKey(int id)
        {
            TEntity itemToBeDeleted = this.DoFindByKey(id);

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
