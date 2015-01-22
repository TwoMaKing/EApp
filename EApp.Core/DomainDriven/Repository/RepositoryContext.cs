using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Domain;

namespace EApp.Core.DomainDriven.Repository
{
    public abstract class RepositoryContext : IRepositoryContext
    {
        private Guid id;

        private ThreadLocal<Dictionary<IEntity, IUnitOfWorkRepository>> localAddedCollection = 
                new ThreadLocal<Dictionary<IEntity, IUnitOfWorkRepository>>(() => new Dictionary<IEntity, IUnitOfWorkRepository>());

        private ThreadLocal<Dictionary<IEntity, IUnitOfWorkRepository>> localModifiedCollection =
                new ThreadLocal<Dictionary<IEntity, IUnitOfWorkRepository>>(() => new Dictionary<IEntity, IUnitOfWorkRepository>());

        private ThreadLocal<Dictionary<IEntity, IUnitOfWorkRepository>> localDeletedCollection =
                new ThreadLocal<Dictionary<IEntity, IUnitOfWorkRepository>>(() => new Dictionary<IEntity, IUnitOfWorkRepository>());

        private ThreadLocal<bool> committed = new ThreadLocal<bool>(() => false);

        private Dictionary<Type, object> repositoryCaches = new Dictionary<Type, object>();

        private static readonly object lockObject = new object();

        #region Reserved

        //private ThreadLocal<List<object>> localAddedCollection =
        //    new ThreadLocal<List<object>>(() => new List<object>());

        //private ThreadLocal<List<object>> localModifiedCollection = 
        //    new ThreadLocal<List<object>>(() => new List<object>());

        //private ThreadLocal<List<object>> localDeletedCollection =
        //    new ThreadLocal<List<object>>(() => new List<object>());

        //protected List<object> AddedCollection 
        //{
        //    get 
        //    {
        //        return this.localAddedCollection.Value;
        //    }
        //}

        //protected List<object> ModifiedCollection
        //{
        //    get
        //    {
        //        return this.localModifiedCollection.Value;
        //    }
        //}

        //protected List<object> DeletedCollection
        //{
        //    get 
        //    {
        //        return this.localDeletedCollection.Value;
        //    }
        //}

        //public virtual void RegisterAdded(object entity)
        //{
        //    if (!this.localAddedCollection.Value.Contains(entity))
        //    {
        //        this.localAddedCollection.Value.Add(entity);

        //        this.committed = false;
        //    }
        //}

        //public virtual void RegisterModified(object entity)
        //{
        //    if (!this.localModifiedCollection.Value.Contains(entity) &&
        //        !this.localAddedCollection.Value.Contains(entity))
        //    {
        //        this.localModifiedCollection.Value.Add(entity);

        //        this.committed = false;
        //    }
        //}

        //public virtual void RegisterDeleted(object entity)
        //{
        //    if (this.localAddedCollection.Value.Contains(entity))
        //    {
        //        this.localAddedCollection.Value.Remove(entity);
        //        return;
        //    }

        //    if (this.localModifiedCollection.Value.Contains(entity))
        //    {
        //        this.localModifiedCollection.Value.Remove(entity);
        //    }

        //    if (!localDeletedCollection.Value.Contains(entity))
        //    {
        //        localDeletedCollection.Value.Add(entity);

        //        this.committed = false;
        //    }
        //}

        #endregion

        public Guid Id
        {
            get 
            {
                if (this.id == null)
                {
                    this.id = Guid.NewGuid();
                }

                return this.id;
            }
        }

        protected Dictionary<IEntity, IUnitOfWorkRepository> AddedCollection
        {
            get
            {
                return this.localAddedCollection.Value;
            }
        }

        protected Dictionary<IEntity, IUnitOfWorkRepository> ModifiedCollection
        {
            get
            {
                return this.localModifiedCollection.Value;
            }
        }

        protected Dictionary<IEntity, IUnitOfWorkRepository> DeletedCollection
        {
            get
            {
                return this.localDeletedCollection.Value;
            }
        }

        protected abstract void Dispose(bool disposing);

        public bool Committed
        {
            get 
            {
                return this.committed.Value;
            }
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity<int>, IEntity
        {
            lock (lockObject)
            { 
                Type entityType = typeof(TEntity);

                if (!this.repositoryCaches.ContainsKey(entityType))
                {
                    IRepository<TEntity> repository = this.CreateRepository<TEntity>();

                    this.repositoryCaches.Add(entityType, repository);
                }

                return (IRepository<TEntity>)this.repositoryCaches[entityType];
            }
        }

        public virtual void RegisterAdded(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            if (!this.localAddedCollection.Value.ContainsKey(entity))
            {
                this.localAddedCollection.Value.Add(entity, unitOfWorkRepository);

                this.committed.Value = false;
            }
        }

        public virtual void RegisterModified(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            if (!this.localModifiedCollection.Value.ContainsKey(entity) &&
                !this.localDeletedCollection.Value.ContainsKey(entity))
            {
                this.localModifiedCollection.Value.Add(entity, unitOfWorkRepository);

                this.committed.Value = false;
            }
        }

        public virtual void RegisterDeleted(IEntity entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            if (this.localAddedCollection.Value.ContainsKey(entity))
            {
                this.localAddedCollection.Value.Remove(entity);

                return;
            }

            if (this.localModifiedCollection.Value.ContainsKey(entity))
            {
                this.localModifiedCollection.Value.Remove(entity);
            }

            if (!this.localDeletedCollection.Value.ContainsKey(entity))
            {
                this.localDeletedCollection.Value.Add(entity, unitOfWorkRepository);

                this.committed.Value = false;
            }
        }

        public abstract void Commit();

        public abstract void Rollback();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class, IEntity<int>, IEntity;

    }

}
