using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EApp.Core.Application;
using EApp.Core;

namespace EApp.Domain.Core.Repositories
{
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        private Guid id = Guid.NewGuid();

        private ThreadLocal<Dictionary<IEnumerable<object>, IRepositoryPersistence>> addedPersistenceCollection =
                new ThreadLocal<Dictionary<IEnumerable<object>, IRepositoryPersistence>>(() => new Dictionary<IEnumerable<object>, IRepositoryPersistence>());

        private ThreadLocal<Dictionary<IEnumerable<object>, IRepositoryPersistence>> modifiedPersistenceCollection =
                new ThreadLocal<Dictionary<IEnumerable<object>, IRepositoryPersistence>>(() => new Dictionary<IEnumerable<object>, IRepositoryPersistence>());

        private ThreadLocal<Dictionary<IEnumerable<object>, IRepositoryPersistence>> deletedPersistenceCollection =
                new ThreadLocal<Dictionary<IEnumerable<object>, IRepositoryPersistence>>(() => new Dictionary<IEnumerable<object>, IRepositoryPersistence>());

        private ThreadLocal<bool> committed = new ThreadLocal<bool>(() => false);

        private Dictionary<Type, object> repositoryCaches = new Dictionary<Type, object>();

        private static readonly object lockObject = new object();

        public Guid Id
        {
            get 
            {
                return this.id;
            }
        }

        public virtual bool DistributedTransactionSupported
        {
            get 
            { 
                return false; 
            }
        }

        public bool Committed
        {
            get 
            {
                return this.committed.Value;
            }
            protected set
            {
                this.committed.Value = value;
            }
        }

        public IRepository<TAggregateRoot> GetRepository<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot<int>, IAggregateRoot, new()
        {
            lock (lockObject)
            { 
                Type entityType = typeof(TAggregateRoot);

                if (!this.repositoryCaches.ContainsKey(entityType))
                {
                    IRepository<TAggregateRoot> repository = this.CreateRepository<TAggregateRoot>();

                    this.repositoryCaches.Add(entityType, repository);
                }

                return (IRepository<TAggregateRoot>)this.repositoryCaches[entityType];
            }
        }

        public virtual void RegisterAdded(IEnumerable<object> objects, IRepositoryPersistence persistence)
        {
            if (!this.addedPersistenceCollection.Value.ContainsKey(objects))
            {
                this.addedPersistenceCollection.Value.Add(objects, persistence);

                this.committed.Value = false;
            }
        }

        public virtual void RegisterModified(IEnumerable<object> objects, IRepositoryPersistence persistence)
        {
            if (!this.modifiedPersistenceCollection.Value.ContainsKey(objects) &&
                !this.deletedPersistenceCollection.Value.ContainsKey(objects))
            {
                this.modifiedPersistenceCollection.Value.Add(objects, persistence);

                this.committed.Value = false;
            }
        }

        public virtual void RegisterDeleted(IEnumerable<object> objects, IRepositoryPersistence persistence)
        {
            if (this.addedPersistenceCollection.Value.ContainsKey(objects))
            {
                this.addedPersistenceCollection.Value.Remove(objects);

                return;
            }

            if (this.modifiedPersistenceCollection.Value.ContainsKey(objects))
            {
                this.modifiedPersistenceCollection.Value.Remove(objects);
            }

            if (!this.deletedPersistenceCollection.Value.ContainsKey(objects))
            {
                this.deletedPersistenceCollection.Value.Add(objects, persistence);

                this.committed.Value = false;
            }
        }

        public void Commit() 
        {
            this.Persist();

            this.DoCommit();

            this.committed.Value = true;
        }

        public void Rollback() 
        {
            this.DoRollback();

            this.committed.Value = false;
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected Dictionary<IEnumerable<object>, IRepositoryPersistence> AddedPersistenceCollection
        {
            get
            {
                return this.addedPersistenceCollection.Value;
            }
        }

        protected Dictionary<IEnumerable<object>, IRepositoryPersistence> ModifiedPersistenceCollection
        {
            get
            {
                return this.modifiedPersistenceCollection.Value;
            }
        }

        protected Dictionary<IEnumerable<object>, IRepositoryPersistence> DeletedPersistenceCollection
        {
            get
            {
                return this.deletedPersistenceCollection.Value;
            }
        }

        protected virtual void Persist()
        {
            if (this.AddedPersistenceCollection != null &&
                this.AddedPersistenceCollection.Count > 0)
            {
                foreach (KeyValuePair<IEnumerable<object>, IRepositoryPersistence> addedPersistence in this.AddedPersistenceCollection)
                {
                    addedPersistence.Value.PersistAddedItems(addedPersistence.Key);
                }
            }

            if (this.ModifiedPersistenceCollection != null &&
                this.ModifiedPersistenceCollection.Count > 0)
            {
                foreach (KeyValuePair<IEnumerable<object>, IRepositoryPersistence> modifiedPersistence in this.ModifiedPersistenceCollection)
                {
                    modifiedPersistence.Value.PersistModifiedItems(modifiedPersistence.Key);
                }
            }

            if (this.DeletedPersistenceCollection != null &&
                this.DeletedPersistenceCollection.Count > 0)
            {
                foreach (KeyValuePair<IEnumerable<object>, IRepositoryPersistence> deletedPersistence in this.DeletedPersistenceCollection)
                {
                    deletedPersistence.Value.PersistDeletedItems(deletedPersistence.Key);
                }
            }
        }

        protected abstract void DoCommit();

        protected abstract void DoRollback();

        protected override void Dispose(bool disposing) 
        {
            if (disposing)
            {
                this.addedPersistenceCollection.Dispose();
                this.modifiedPersistenceCollection.Dispose();
                this.deletedPersistenceCollection.Dispose();
                this.committed.Dispose();

                this.repositoryCaches.Clear();
            }
        }

        protected abstract IRepository<TAggregateRoot> CreateRepository<TAggregateRoot>()
            where TAggregateRoot : class, IAggregateRoot<int>, IAggregateRoot, new();

    }

}
