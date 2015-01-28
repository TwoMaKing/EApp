using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using Microsoft.Practices.Unity;
using MongoDB;
using MongoDB.Configuration;

namespace EApp.Repositories.MongoDB
{
    public class MongoDBRepositoryContext : RepositoryContext, IMongoDBRepositoryContext
    {
        private Mongo mongo;

        private string databaseName;

        public MongoDBRepositoryContext(string connectionString, string databaseName) 
        {
            this.databaseName = databaseName;

            this.mongo = new Mongo(connectionString);

            this.mongo.Connect();
        }

        public IMongoDatabase MongoDatabase
        {
            get 
            {
                return this.mongo[this.databaseName];
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.mongo != null)
                {
                    this.mongo.Disconnect();
                    this.mongo.Dispose();
                }
            }
        }

        protected override void DoCommit()
        {
            if (this.AddedCollection != null &&
                this.AddedCollection.Count > 0)
            {
                foreach (KeyValuePair<IEntity, IUnitOfWorkRepository> addedUnitOfWorkRepository in this.AddedCollection)
                {
                    addedUnitOfWorkRepository.Value.PersistAddedItem(addedUnitOfWorkRepository.Key);
                }
            }

            if (this.ModifiedCollection != null &&
                this.ModifiedCollection.Count > 0)
            {
                foreach (KeyValuePair<IEntity, IUnitOfWorkRepository> modifiedUnitOfWorkRepository in this.ModifiedCollection)
                {
                    modifiedUnitOfWorkRepository.Value.PersistModifiedItem(modifiedUnitOfWorkRepository.Key);
                }
            }

            if (this.DeletedCollection != null &&
                this.DeletedCollection.Count > 0)
            {
                foreach (KeyValuePair<IEntity, IUnitOfWorkRepository> deletedUnitOfWorkRepository in this.DeletedCollection)
                {
                    deletedUnitOfWorkRepository.Value.PersistDeletedItem(deletedUnitOfWorkRepository.Key);
                }
            }

            this.mongo.Disconnect();

            this.mongo.Dispose();
        }

        protected override void DoRollback()
        {
            base.Rollback();
        }

        protected override IRepository<TEntity> CreateRepository<TEntity>()
        {
            IEnumerable<Type> repositoryTypesMapTo =
                EAppRuntime.Instance.CurrentApp.ObjectContainer.TypesMapTo.Where(t => typeof(MongoDBRepository<TEntity>).IsAssignableFrom(t));

            Type repositoryType = repositoryTypesMapTo.FirstOrDefault();

            IUnityContainer unityContainer = EAppRuntime.Instance.CurrentApp.ObjectContainer.GetWrapperContainer<IUnityContainer>();

            return (IRepository<TEntity>)unityContainer.Resolve(repositoryType, new DependencyOverride<IRepositoryContext>(this));
        }
    }
}
