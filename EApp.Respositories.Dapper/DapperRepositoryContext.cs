using Dappers;
using EApp.Common.Util;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Exceptions;
using EApp.Domain.Core.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EApp.Respositories.Dapper
{
    public class DapperRepositoryContext : RepositoryContext, IDapperRepositoryContext
    {
        private string providerName;

        private string connectionString;

        private List<CommandDefinition> commandDefinitionList = new List<CommandDefinition>();

        public DapperRepositoryContext() : this(ConfigurationManager.ConnectionStrings[0].Name)
        {
        }

        public DapperRepositoryContext(string connectionStringSectionName)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringSectionName];

            if (!connectionStringSettings.ProviderName.HasValue())
            {
                throw new ConfigException("");
            }

            if (!connectionStringSettings.ConnectionString.HasValue())
            {
                throw new ConfigException("");
            }

            this.providerName = connectionStringSettings.ProviderName;

            this.connectionString = connectionStringSettings.ConnectionString;
        }

        public DapperRepositoryContext(string providerName, string connectionString)
        {
            if (!providerName.HasValue())
            {
                throw new ConfigException("");
            }

            if (!connectionString.HasValue())
            {
                throw new ConfigException("");
            }

            this.providerName = providerName;
            this.connectionString = connectionString;
        }

        protected override void DoCommit()
        {
            using (IDbConnection connection = this.CreateConnection())
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        for (int commandIndex = 0; commandIndex < commandDefinitionList.Count; commandIndex++)
                        {
                            CommandDefinition commandDefinition = commandDefinitionList[commandIndex];

                            connection.Execute(commandDefinition);
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        throw e;
                    }
                    finally
                    {
                        this.commandDefinitionList.Clear();
                        this.CloseConnection(connection);
                    }
                }
            }
        }

        protected override void DoRollback()
        {
            this.commandDefinitionList.Clear();
            this.Committed = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.commandDefinitionList.Clear();
            }

            base.Dispose(disposing);
        }

        protected override IRepository<TAggregateRoot> CreateRepository<TAggregateRoot>()
        {
            IEnumerable<Type> repositoryTypesMapTo =
                EAppRuntime.Instance.CurrentApp.ObjectContainer.TypesMapTo.Where(t => typeof(DapperRepository<TAggregateRoot>).IsAssignableFrom(t));

            Type repositoryType = repositoryTypesMapTo.FirstOrDefault();

            IUnityContainer unityContainer = EAppRuntime.Instance.CurrentApp.ObjectContainer.GetWrapperContainer<IUnityContainer>();

            return (IRepository<TAggregateRoot>)unityContainer.Resolve(repositoryType, new DependencyOverride<IRepositoryContext>(this));
        }

        #region IDapperRepositoryContext members

        public IDbConnection CreateConnection()
        {
            IDbConnection dbConnection = DbProviderFactories.GetFactory(this.providerName).CreateConnection();
            dbConnection.ConnectionString = this.connectionString;

            return dbConnection;
        }

        public void CloseConnection(IDbConnection connection)
        {
            if (connection != null &&
                connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public T Query<T>(string querySql, object @params)
        {
            using (IDbConnection dbConnection = this.CreateConnection())
            {
                T obj = dbConnection.Query<T>(querySql, @params).FirstOrDefault();

                this.CloseConnection(dbConnection);

                return obj;
            }
        }

        public void Insert(string insertSql, object obj)
        {
            CommandDefinition commandDefinition = new CommandDefinition(insertSql, obj);

            commandDefinitionList.Add(commandDefinition);
        }

        public void Update(string updateSql, object obj)
        {
            CommandDefinition commandDefinition = new CommandDefinition(updateSql, obj);

            commandDefinitionList.Add(commandDefinition);
        }

        public void Delete(string deleteSql, object obj)
        {
            CommandDefinition commandDefinition = new CommandDefinition(deleteSql, obj);

            commandDefinitionList.Add(commandDefinition);
        }

        #endregion
    }
}
