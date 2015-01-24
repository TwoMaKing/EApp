using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Cache;
using EApp.Common.Util;
using EApp.Core;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.QuerySepcifications;
using EApp.Data;

namespace EApp.Repositories.SqlServer
{
    /// <summary>
    /// Repository used for Sql Server.
    /// </summary>
    public abstract class SqlServerRepository<TEntity> : Repository<TEntity>, IUnitOfWorkRepository 
        where TEntity : class, IEntity
    {
        protected delegate void AppendChildToEntity(TEntity entity, int childEntityId);

        private ISqlServerRepositoryContext sqlServerRepositoryContext;

        private ICacheManager cacheManager = CacheFactory.GetCacheManager();

        public SqlServerRepository(IRepositoryContext repositoryContext) : base(repositoryContext) 
        {
            if (repositoryContext is ISqlServerRepositoryContext)
            {
                this.sqlServerRepositoryContext = repositoryContext as ISqlServerRepositoryContext;
            }
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
            this.sqlServerRepositoryContext.RegisterAdded(item, this);
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

        protected override TEntity DoFindByKey(int id)
        {
            string cacheEntityId = typeof(TEntity).Name + "_" + id.ToString();

            if (this.cacheManager.ContainsKey(cacheEntityId)) 
            {
                return this.cacheManager.GetItem<TEntity>(cacheEntityId);
            }

            TEntity currentEntity = default(TEntity);

            string querySqlById = this.GetEntityQuerySqlById();

            using(IDataReader reader = DbGateway.Default.ExecuteReader(querySqlById, new object[] { id }))
            {
                var currentEntities = this.BuildEntitiesFromDataReader(reader);

                if (currentEntities != null &&
                    currentEntities.Count() > 0)
                {
                    currentEntity = currentEntities.FirstOrDefault();

                    Dictionary<string, AppendChildToEntity> childCallbacks = this.BuildChildCallbacks();

                    if (childCallbacks != null &&
                        childCallbacks.Count > 0)
                    {
                        foreach (KeyValuePair<string, AppendChildToEntity> callbackItem in childCallbacks)
                        {
                            string childEntityForeignKey = reader[callbackItem.Key].ToString();

                            int childEntityId = Convertor.ConvertToInteger(childEntityForeignKey).Value;

                            callbackItem.Value(currentEntity, childEntityId);
                        }
                    }
                }

                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }

            this.cacheManager.AddItem<TEntity>(cacheEntityId, currentEntity);

            return currentEntity;
        }

        protected abstract string GetEntityQuerySqlById();

        protected virtual IEnumerable<TEntity> BuildEntitiesFromDataReader(IDataReader dataReader) 
        {
            if (dataReader == null ||
                dataReader.IsClosed)
            {
                return null;
            }

            List<TEntity> entities = new List<TEntity>();

            while (dataReader.Read())
            {
                TEntity entity = this.BuildEntityFromDataReader(dataReader);

                entities.Add(entity);
            }

            return entities;
        }

        protected abstract TEntity BuildEntityFromDataReader(IDataReader dataReader);

        protected abstract Dictionary<string, AppendChildToEntity> BuildChildCallbacks();

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
