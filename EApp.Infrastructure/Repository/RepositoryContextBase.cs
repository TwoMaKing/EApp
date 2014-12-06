using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.UnitOfWork;

namespace EApp.Infrastructure.Repository
{
    public abstract class RepositoryContextBase<TIdentityKey> : IRepositoryContext<TIdentityKey> , IUnitOfWork
    {
        private TIdentityKey id;

        private bool committed;

        private ThreadLocal<List<IEntity<TIdentityKey>>> localAddedCollection = 
            new ThreadLocal<List<IEntity<TIdentityKey>>>(() => new List<IEntity<TIdentityKey>>());

        private ThreadLocal<List<IEntity<TIdentityKey>>> localModifiedCollection = 
            new ThreadLocal<List<IEntity<TIdentityKey>>>(() => new List<IEntity<TIdentityKey>>());

        private ThreadLocal<List<IEntity<TIdentityKey>>> localDeletedCollection = 
            new ThreadLocal<List<IEntity<TIdentityKey>>>(() => new List<IEntity<TIdentityKey>>());

        private ThreadLocal<bool> localCommitted = new ThreadLocal<bool>(() => true);

        public TIdentityKey Id
        {
            get 
            {
                if (this.id == null)
                {
                    this.id = this.CreateIdentityKey();
                }

                return this.id;
            }
        }


        protected List<IEntity<TIdentityKey>> AddedCollection 
        {
            get 
            {
                return this.localAddedCollection.Value;
            }
        }

        protected List<IEntity<TIdentityKey>> ModifiedCollection
        {
            get
            {
                return this.localModifiedCollection.Value;
            }
        }

        protected List<IEntity<TIdentityKey>> DeletedCollection
        {
            get 
            {
                return this.localDeletedCollection.Value;
            }
        }

        public virtual void RegisterAddedEntity(IEntity<TIdentityKey> entity)
        {
            if (!this.localAddedCollection.Value.Contains(entity))
            {
                this.localAddedCollection.Value.Add(entity);

                this.committed = false;
            }
        }

        public virtual void RegisterModifiedEntity(IEntity<TIdentityKey> entity)
        {
            if (!this.localModifiedCollection.Value.Contains(entity))
            {
                this.localModifiedCollection.Value.Add(entity);

                this.committed = false;
            }
        }

        public virtual void RegisterDeletedEntity(IEntity<TIdentityKey> entity)
        {
            if (localAddedCollection.Value.Contains(entity))
            {
                localAddedCollection.Value.Remove(entity);
            }

            if (!localDeletedCollection.Value.Contains(entity))
            {
                localDeletedCollection.Value.Add(entity);

                this.committed = false;
            }
        }

        public bool Committed
        {
            get 
            {
                return this.committed;
            }
        }

        public abstract void Commit();

        public abstract void Rollback();

        protected abstract TIdentityKey CreateIdentityKey();
    }

    public abstract class RepositoryContextBase : RepositoryContextBase<Guid>, IRepositoryContext
    {
        protected override Guid CreateIdentityKey()
        {
            return Guid.NewGuid();
        }
    }
}
