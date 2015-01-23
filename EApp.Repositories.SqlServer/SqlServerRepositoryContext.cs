using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.QuerySepcifications;
using EApp.Data;

namespace EApp.Repositories.SqlServer
{
    /// <summary>
    /// Repository Context for Sql Server.
    /// </summary>
    public class SqlServerRepositoryContext : RepositoryContext, ISqlServerRepositoryContext
    {
        private DbConnection dbConnection;

        private DbTransaction dbTransaction;

        public SqlServerRepositoryContext() 
        {
            this.dbConnection = DbGateway.Default.OpenConnection();

            this.dbTransaction = this.dbConnection.BeginTransaction();
        }

        public DbTransaction Transaction
        {
            get 
            {
                if (this.dbConnection.State == ConnectionState.Closed)
                {
                    this.dbConnection = DbGateway.Default.OpenConnection();

                    this.dbTransaction = this.dbConnection.BeginTransaction();
                }

                return this.dbTransaction;
            }
        }

        public override void Commit()
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

            this.dbTransaction.Commit();
        }

        public override void Rollback()
        {
            this.dbTransaction.Rollback();   
        }

        protected override void Dispose(bool disposing)
        {
            DbGateway.Default.CloseConnection(this.dbConnection);
        }

        protected override IRepository<TEntity> CreateRepository<TEntity>(string name = "")
        {
            IEnumerable<Type> repositoryTypes = 
                EAppRuntime.Instance.CurrentApp.ObjectContainer.TypesFrom.Where(t=> typeof(IRepository<TEntity>).IsAssignableFrom(t));

            Type repositoryType = null;

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrWhiteSpace(name))
            {
                repositoryType = repositoryTypes.FirstOrDefault();
            }
            else
            { 
                
            }

            return (IRepository<TEntity>)EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve(repositoryType);
        }
    }
}
