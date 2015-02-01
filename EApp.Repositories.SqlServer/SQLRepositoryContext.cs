using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Domain.Core.Repositories;
using Microsoft.Practices.Unity;

namespace EApp.Repositories.SQL
{
    /// <summary>
    /// Repository Context for Sql Server.
    /// </summary>
    public class SQLRepositoryContext : RepositoryContext, ISQLRepositoryContext
    {
        private ISQLRepositoryContextDbProvider dbProvider;

        public SQLRepositoryContext(ISQLRepositoryContextDbProvider dbProvider) 
        {
            this.dbProvider = dbProvider;
        }

        public SQLRepositoryContext() : this(new SQLRepositoryContextDbProvider()) { }

        public ISQLRepositoryContextDbProvider DbProvider
        {
            get 
            { 
                return this.dbProvider; 
            }
        }

        public override bool DistributedTransactionSupported
        {
            get
            {
                return true;
            }
        }

        protected override void DoCommit()
        {
            if (this.DistributedTransactionSupported)
            {

            }
            else
            {
                this.DbProvider.DbTransaction.Commit();
            }
        }

        protected override void DoRollback()
        {
            this.DbProvider.DbTransaction.Rollback();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DbProvider.DbTransaction != null &
                    this.DbProvider.DbTransaction.Connection != null &&
                    this.DbProvider.DbTransaction.Connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        this.DbProvider.DbTransaction.Connection.Close();
                        this.DbProvider.DbTransaction.Connection.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    this.DbProvider.DbTransaction.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        protected override IRepository<TAggregateRoot> CreateRepository<TAggregateRoot>()
        {
            IEnumerable<Type> repositoryTypesMapTo =
                EAppRuntime.Instance.CurrentApp.ObjectContainer.TypesMapTo.Where(t => typeof(SQLRepository<TAggregateRoot>).IsAssignableFrom(t));

            Type repositoryType = repositoryTypesMapTo.FirstOrDefault();

            IUnityContainer unityContainer = EAppRuntime.Instance.CurrentApp.ObjectContainer.GetWrapperContainer<IUnityContainer>();

            return (IRepository<TAggregateRoot>)unityContainer.Resolve(repositoryType, new DependencyOverride<IRepositoryContext>(this));
        }
    }
}
