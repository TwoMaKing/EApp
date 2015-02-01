using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using EApp.Data;

namespace EApp.Repositories.SQL
{
    public class SQLRepositoryContextDbProvider : ISQLRepositoryContextDbProvider
    {
        private static readonly object lockObject = new object();

        private Database database;

        private DbGateway dbGateway;

        private List<DbCommand> dbCommands = new List<DbCommand>();

        private DbTransaction dbTransaction;

        public SQLRepositoryContextDbProvider() 
        {
            this.database = Database.Default;

            this.dbGateway = new DbGateway(database);
        }

        public SQLRepositoryContextDbProvider(string connectionStringSectionName) 
        {
            this.database = new Database(EApp.Data.DbProviderFactory.CreateDbProvider(connectionStringSectionName));

            this.dbGateway = new DbGateway(this.database);
        }

        public SQLRepositoryContextDbProvider(DatabaseType databaseType, string connectionString) 
        {
            string dbProviderTypeName = "";

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

            this.dbGateway = new DbGateway(this.database);
        }

        public DbTransaction DbTransaction
        {
            get 
            {
                if (this.dbTransaction == null)
                {
                    this.dbTransaction = DbGateway.Default.BeginTransaction();
                }

                return this.dbTransaction;
            }
        }

        public IEnumerable<DbCommand> DbCommands
        {
            get 
            {
                return this.dbCommands;
            }
        }

        public void Insert(string table, string[] columns, object[] values)
        {
            this.dbGateway.Insert(table, columns, values, this.dbTransaction);
        }

        public void Insert(string table, object[] values)
        {
            this.dbGateway.Insert(table, values, this.dbTransaction);
        }

        public void Update(string table, 
                           string[] columns, 
                           object[] values, 
                           string whereSql, 
                           object[] whereParamValues)
        {
            this.dbGateway.Update(table, columns, values, whereSql, whereParamValues, this.dbTransaction);
        }

        public void Delete(string table, string whereSql, object[] whereParamValues)
        {
            this.dbGateway.Delete(table, whereSql, whereParamValues, this.dbTransaction);
        }

        public void AddDbCommand(DbCommand dbCommand, string[] paramNames = null, object[] paramValues = null)
        {
            lock (lockObject)
            {
                if (paramNames != null &&
                    paramValues != null &&
                    paramNames.Length.Equals(paramValues.Length))
                {
                    for (int paramIndex = 0; paramIndex < paramNames.Length; paramIndex++)
                    {
                        this.database.AddInParameter(dbCommand, paramNames[paramIndex], paramValues[paramIndex]);
                    }
                }

                dbCommand.Transaction = this.dbTransaction;
                dbCommands.Add(dbCommand);
            }
        }

        public string BuildParameterName(string paramName)
        {
            return this.database.DBProvider.BuildParameterName(paramName);
        }
    }
}
