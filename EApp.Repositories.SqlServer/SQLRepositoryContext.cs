using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Data.Mapping;
using EApp.Data.Queries;
using EApp.Domain.Core.Repositories;
using Microsoft.Practices.Unity;

namespace EApp.Repositories.SQL
{
    /// <summary>
    /// Repository Context for Sql Server.
    /// </summary>
    public class SqlRepositoryContext : RepositoryContext, ISqlRepositoryContext
    {
        private readonly char[] Parameter_Prefixes = new char[] { '@', '?', ':' };

        private Database database;

        private List<DbCommand> dbCommands = new List<DbCommand>();

        private static readonly object lockObject = new object();

        [InjectionConstructor()]
        public SqlRepositoryContext() : this(ConfigurationManager.ConnectionStrings[0].Name) { }

        public SqlRepositoryContext(string connectionStringSectionName) 
        {
            this.database = new Database(EApp.Data.DbProviderFactory.CreateDbProvider(connectionStringSectionName)); 
        }

        public SqlRepositoryContext(DatabaseType databaseType, string connectionString) 
        {
            string dbProviderTypeName = typeof(EApp.Data.SqlServer.SqlServerDbProvider).FullName;

            if (databaseType == DatabaseType.SqlServer)
            {
                dbProviderTypeName = typeof(EApp.Data.SqlServer.SqlServerDbProvider).FullName;
            }
            else if (databaseType == DatabaseType.Oracle)
            {
                dbProviderTypeName = typeof(EApp.Data.Oracle.OracleDbProvider).FullName;
            }
            else if (databaseType == DatabaseType.MySql)
            {
                dbProviderTypeName = typeof(EApp.Data.MySql.MySqlDbProvider).FullName;
            }
            else if (databaseType == DatabaseType.SqlLite)
            {
                dbProviderTypeName = typeof(EApp.Data.SqlLite.SqlLiteDbProvider).FullName;
            }

            this.database = new Database(EApp.Data.DbProviderFactory.CreateDbProvider(null, dbProviderTypeName, connectionString));
        }

        public override bool DistributedTransactionSupported
        {
            get
            {
                return true;
            }
        }

        #region Protected Methods

        protected override void DoCommit()
        {
            using (DbConnection dbConnection = this.database.CreateConnection(true))
            {
                using (DbTransaction dbTransaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        if (this.dbCommands != null)
                        {
                            foreach (DbCommand dbCommand in this.dbCommands)
                            {
                                this.database.ExecuteNonQuery(dbCommand, dbTransaction);
                            }
                        }

                        dbTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();

                        throw e;
                    }
                    finally
                    {
                        this.dbCommands.Clear();
                    }
                }
            }
        }

        protected override void DoRollback()
        {
            this.Committed = false;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override IRepository<TAggregateRoot> CreateRepository<TAggregateRoot>()
        {
            IEnumerable<Type> repositoryTypesMapTo =
                EAppRuntime.Instance.CurrentApp.ObjectContainer.TypesMapTo.Where(t => typeof(SqlRepository<TAggregateRoot>).IsAssignableFrom(t));

            Type repositoryType = repositoryTypesMapTo.FirstOrDefault();

            IUnityContainer unityContainer = EAppRuntime.Instance.CurrentApp.ObjectContainer.GetWrapperContainer<IUnityContainer>();

            return (IRepository<TAggregateRoot>)unityContainer.Resolve(repositoryType,  new DependencyOverride<IRepositoryContext>(this));
        }

        #endregion

        #region ISQLRepositoryContext

        public DbConnection CreateConnection()
        {
            return this.database.CreateConnection();
        }

        public void CloseConnection(DbConnection connection)
        {
            this.database.CloseConnection(connection);
        }

        public WhereClauseBuildResult GetWhereClauseSql<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> predicate) where TAggregateRoot : class, new()
        {
            return this.database.DBProvider.CreateWhereClauseBuilder<TAggregateRoot>().BuildWhereClause(predicate);
        }

        public string GetOrderByClauseSql<TAggregateRoot>(Expression<Func<TAggregateRoot, dynamic>> predicate) where TAggregateRoot : class, new()
        {
            return this.database.DBProvider.CreateWhereClauseBuilder<TAggregateRoot>().BuildOrderByClause(predicate);
        }

        public IDataReader Select(string querySql, object[] whereParamValues = null, DbTransaction transaction = null)
        {
            string adjustedQuerySql = this.BuildParameterPrefix(querySql);

            string[] whereParamNames = this.database.GetParsedParamNames(adjustedQuerySql);

            DbCommand queryCommand = this.PrepareSqlStringCommand(querySql, whereParamNames, null, whereParamValues);

            if (transaction != null)
            {
                return this.database.ExecuteReader(queryCommand, transaction);
            }
            else
            {
                return this.database.ExecuteReader(queryCommand);
            }
        }

        public IDataReader Select(string table, string[] columns, DbTransaction transaction = null)
        {
            return Select(table, columns, null, null, transaction);
        }

        public IDataReader Select(string table, string[] columns, string where, object[] whereParamValues, DbTransaction transaction = null)
        {
            return Select(table, columns, where, whereParamValues, null, transaction);
        }

        public IDataReader Select(string table, string[] columns, string where, object[] whereParamValues, string orderBy, DbTransaction transaction = null)
        {
            return Select(table, columns, where, whereParamValues, orderBy, 1, int.MaxValue, null, true, transaction);
        }

        public IDataReader Select(string table, 
                                  string[] columns, 
                                  string where, 
                                  object[] whereParamValues, 
                                  string orderBy, 
                                  int pageNumber, 
                                  int pageSize, 
                                  string identityColumn, 
                                  bool identityColumnIsNumber = true, 
                                  DbTransaction transaction = null)
        {
            ISqlStatementFactory sqlStatementFactory = this.database.GetSqlStatementFactory();

            string selectRangeSqlStatement = sqlStatementFactory.CreateSelectRangeStatement(table, where, orderBy, pageSize, (pageNumber - 1) * pageSize, identityColumn, identityColumnIsNumber, null, columns);

            string[] whereParamNames = this.database.DiscoverParams(where);

            DbCommand selectRangeCommand = this.PrepareSqlStringCommand(selectRangeSqlStatement,
                                                                        whereParamNames.ToArray(),
                                                                        null,
                                                                        whereParamValues.ToArray());
            if (transaction != null)
            {
                return this.database.ExecuteReader(selectRangeCommand, transaction);
            }
            else
            {
                return this.database.ExecuteReader(selectRangeCommand);
            }
        }

        public void Insert(string table, object[] values)
        {
            this.Insert(table, null, values);
        }

        public void Insert(string table, string[] columns, object[] values)
        {
            string insertSqlStatement = this.GetInsertSql(table, columns);

            string[] paramNames = null;

            if (columns == null)
            {
                paramNames = new string[values.Length];

                string paramNamesSql = string.Empty;

                for (int paramIndex = 0; paramIndex < values.Length; paramIndex++)
                {
                    paramNames[paramIndex] = "@column_" + (paramIndex + 1).ToString();

                    paramNamesSql += paramNames[paramIndex] + ",";
                }

                insertSqlStatement = string.Format(insertSqlStatement, paramNamesSql.TrimEnd(','));
            }
            else
            {
                paramNames = columns;
            }

            this.AddDbCommand(this.PrepareSqlStringCommand(insertSqlStatement,
                                                           paramNames,
                                                           null,
                                                           values));
        }

        public void Update(string table, string[] columns, object[] values)
        {
            this.Update(table, columns, values, null, null);
        }

        public void Update(string table, string[] columns, object[] values, string whereSql, object[] whereParamValues)
        {
            whereSql = this.BuildParameterPrefix(whereSql);

            string updateSqlStatement = this.GetUpdateSql(table, columns, whereSql);

            string[] whereParamNames = this.database.GetParsedParamNames(whereSql);

            List<string> allParamNames = new List<string>();
            List<object> allParamValues = new List<object>();

            for (int columnIndex = 0; columnIndex < columns.Length; columnIndex++)
            {
                allParamNames.Add(columns[columnIndex]);
                allParamValues.Add(values[columnIndex]);
            }

            if (whereParamNames != null)
            {
                for (int whereParamIndex = 0; whereParamIndex < whereParamNames.Length; whereParamIndex++)
                {
                    allParamNames.Add(whereParamNames[whereParamIndex]);
                    allParamValues.Add(whereParamValues[whereParamIndex]);
                }
            }

            DbCommand updateCommand = this.PrepareSqlStringCommand(updateSqlStatement,
                                                                   allParamNames.ToArray(), 
                                                                   null,
                                                                   allParamValues.ToArray());
            this.AddDbCommand(updateCommand);
        }

        public void Delete(string table)
        {
            this.Delete(table, null, null);
        }

        public void Delete(string table, string whereSql, object[] whereParamValues)
        {
            whereSql = this.BuildParameterPrefix(whereSql);

            string deleteSqlStatement = this.GetDeleteSql(table, whereSql);

            string[] whereParamNames = this.database.GetParsedParamNames(whereSql);

            DbCommand deleteCommand = this.PrepareSqlStringCommand(deleteSqlStatement,
                                                                   whereParamNames,
                                                                   null,
                                                                   whereParamNames == null ? 
                                                                   null : whereParamValues);

            this.AddDbCommand(deleteCommand);
        }

        public void ExecuteNonQuery(string commandText, object[] paramValues = null)
        {
            string sqlCommandtext = this.BuildParameterPrefix(commandText);

            string[] paramNames = this.database.DiscoverParams(sqlCommandtext);

            DbCommand command = this.PrepareSqlStringCommand(sqlCommandtext, paramNames, null, paramValues);

            this.AddDbCommand(command);
        }

        #endregion

        #region Private methods

        private string BuildParameterPrefix(string sqlCommandText)
        {
            if (string.IsNullOrEmpty(sqlCommandText) ||
                string.IsNullOrWhiteSpace(sqlCommandText))
            {
                return null;
            }

            return SqlQueryUtils.ReplaceDatabaseTokens(sqlCommandText,
                                                       this.database.DBProvider.ParameterLeftToken,
                                                       this.database.DBProvider.ParameterRightToken,
                                                       this.database.DBProvider.ParameterPrefix,
                                                       this.database.DBProvider.WildCharToken,
                                                       this.database.DBProvider.WildSingleCharToken);
        }

        private string GetInsertSql(string table, string[] columns) 
        {
            return this.database.GetSqlStatementFactory().CreateInsertStatement(table, columns);
        }

        private string GetUpdateSql(string table, string[] columns,  string whereSql)
        {
            return this.database.GetSqlStatementFactory().CreateUpdateStatement(table, whereSql, columns);
        }

        private string GetDeleteSql(string table, string whereSql)
        {
            return this.database.GetSqlStatementFactory().CreateDeleteStatement(table, whereSql);
        }

        private DbCommand PrepareSqlStringCommand(string sqlCommandText, 
                                                  string[] paramNames, 
                                                  DbType[] paramDbTypes, 
                                                  object[] paramValues)
        {
            if (string.IsNullOrEmpty(sqlCommandText))
            {
                throw new ArgumentNullException("The sql command text cannot be null or empty.");
            }

            if (paramNames != null &&
                paramDbTypes != null &&
               !paramNames.Length.Equals(paramDbTypes.Length))
            {
                throw new ArgumentException("The length of names of parameters should equal the length of db types of parameters.");
            }

            if (paramNames != null &&
               (paramValues == null ||
               !paramNames.Length.Equals(paramValues.Length)))
            {
                throw new ArgumentException("The length of names of parameters should equal the length of values of parameters.");
            }

            DbCommand command = this.database.GetSqlStringCommand(sqlCommandText);

            this.database.CreateAndCacheDbCommandParameters(sqlCommandText, command, paramNames, null, paramValues);

            return command;
        }

        private void AddDbCommand(DbCommand dbCommand)
        {
            lock (lockObject)
            {
                this.dbCommands.Add(dbCommand);
            }
        }

        #endregion

    }
}
