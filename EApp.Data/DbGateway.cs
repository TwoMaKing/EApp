using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EApp.Core.Exceptions;

namespace EApp.Data
{
    /// <summary>
    /// Common Database type used for EApp.
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// MS SQL Server
        /// </summary>
        SqlServer = 0,

        /// <summary>
        /// Oracle
        /// </summary>
        Oracle = 1,

        /// <summary>
        /// MySql
        /// </summary>
        MySql = 2,

        /// <summary>
        /// Sql Lite
        /// </summary>
        SqlLite = 3
    }

    /// <summary>
    /// Data base Gateway. 
    /// Database operation entry.
    /// i.e. for the default database configuration: DbGateway.Default.Insert, Update, Delete, Select.
    /// </summary>
    public class DbGateway
    {
        private static DbGateway defaultDbGateway;

        private Database database;

        private static object lockObject = new object();

        #region Constructors

        static DbGateway() 
        {
            if (ConfigurationManager.ConnectionStrings == null ||
                ConfigurationManager.ConnectionStrings.Count.Equals(0))
            {
                throw new ConfigException("Please provide a connection string including name, provider and connection string.");
            }

            string connectionStringName = ConfigurationManager.ConnectionStrings[1].Name;

            DbProvider dbProvider = DbProviderFactory.CreateDbProvider(connectionStringName);

            defaultDbGateway = new DbGateway(new Database(dbProvider));
        }

        public DbGateway(Database database)
        {
            this.database = database;
        }

        public DbGateway(DatabaseType databaseType, string connectionString)
        {
            this.database = new Database(CreateDbProvider(databaseType, connectionString));
        }

        public DbGateway(string connectionStringName)
        {
            this.database = new Database(DbProviderFactory.CreateDbProvider(connectionStringName));
        }

        #endregion

        #region Private members

        private static DbProvider CreateDbProvider(DatabaseType databaseType, string connectionString) 
        {
            string databaseTypeName = databaseType.ToString();

            ConnectionStringSettings connStrSetting = ConfigurationManager.ConnectionStrings[databaseTypeName];

            string providerName = connStrSetting.ProviderName;

            string[] assemblyAndClassType = providerName.Split(new char[] { ',' });

            if (assemblyAndClassType.Length.Equals(2))
            {
                return DbProviderFactory.CreateDbProvider(assemblyAndClassType[0].Trim(), 
                                                          assemblyAndClassType[1].Trim(), 
                                                          connectionString);
            }
            else
            {
                return DbProviderFactory.CreateDbProvider(string.Empty, providerName, connectionString);
            }
        }


        private DbCommand PrepareSqlStringCommand(string[] paramNames, DbType[] paramDbTypes, object[] paramValues, string sqlCommandText) 
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

            if (this.database.ParameterCache.IsCache(sqlCommandText))
            {
                this.database.ParameterCache.AddParametersFromCache(sqlCommandText, command, paramDbTypes, paramValues);
            }
            else
            {
                lock (lockObject)
                {
                    if (this.database.ParameterCache.IsCache(sqlCommandText))
                    {
                        this.database.ParameterCache.AddParametersFromCache(sqlCommandText, command, paramDbTypes, paramValues);
                    }
                    else
                    {
                        this.database.ParameterCache.CreateAndCacheParameters(sqlCommandText, command, paramNames, paramDbTypes, paramValues);
                    }
                }
            }

            return command;
        }

        private DbCommand PrepareStoredProcedureStringCommand(string procedureName,
                                                              string[] inParamNames, 
                                                              DbType[] inParamDbTypes, 
                                                              object[] inParamValues, 
                                                              string[] outParamNames, 
                                                              DbType[] outParamDbTypes)
        {
            if (string.IsNullOrEmpty(procedureName))
            {
                throw new ArgumentNullException("The stored procedure name cannot be null or empty.");
            }

            if (inParamNames == null ||
                inParamNames.Length == 0)
            {
                throw new ArgumentNullException("The column names of parameters cannot be null or zero.");
            }

            if (inParamValues == null ||
                inParamValues.Length == 0)
            {
                throw new ArgumentNullException("The values of parameters cannot be null or zero.");
            }

            if (inParamDbTypes != null &&
                !inParamNames.Length.Equals(inParamDbTypes.Length))
            {
                throw new ArgumentException("The length of columns of in parameter should equal the length of db types of parameters.");
            }

            if (!inParamNames.Length.Equals(inParamValues.Length))
            {
                throw new ArgumentException("The length of columns of parameter should equal the length of values of parameters.");
            }

            if (outParamNames != null &&
               (outParamDbTypes == null ||
                !outParamNames.Length.Equals(outParamDbTypes.Length)))
            {
                throw new ArgumentException("The length of db types of out parameter should equal the length of names of out parameters.");
            }

            string cacheKey = procedureName + string.Join("_", inParamNames);

            DbCommand command = this.database.GetStoredProcCommand(procedureName);

            if (this.database.ParameterCache.IsCache(cacheKey))
            {
                this.database.ParameterCache.AddParametersFromCache(procedureName, command, inParamDbTypes, inParamValues);
            }
            else
            {
                lock (lockObject)
                {
                    if (this.database.ParameterCache.IsCache(cacheKey))
                    {
                        this.database.ParameterCache.AddParametersFromCache(procedureName, command, inParamDbTypes, inParamValues);
                    }
                    else
                    {
                        this.database.ParameterCache.CreateAndCacheParameters(procedureName, command, inParamNames, inParamDbTypes, inParamValues);
                    }
                }
            }

            //add out params
            if (outParamNames != null)
            {
                for (int i = 0; i < outParamNames.Length; i++)
                {
                    this.database.AddOutParameter(command, outParamNames[i], outParamDbTypes[i], 4000);
                }
            }

            return command;
        }

        private object[] GetOutDbParameterValues(object[] paramNames, DbCommand command) 
        { 
            if (paramNames == null ||
                paramNames.Length.Equals(0) ||
                command == null ||
                command.Parameters == null ||
                command.Parameters.Count.Equals(0))
            {
                return null;
            }

            List<object> outParamValues = new List<object>();
            
            object value;

            foreach (string paramName in paramNames)
            {
                DbParameter dbParameter = command.Parameters[this.database.DBProvider.BuildParameterName(paramName)];

                if (dbParameter.Direction == ParameterDirection.InputOutput ||
                    dbParameter.Direction == ParameterDirection.Output)
                {
                    value = dbParameter.Value;
                }
                else
                {
                    value = null;
                }

                outParamValues.Add(value);
            }

            return outParamValues.ToArray();
        }

        #endregion

        #region Public members

        public static DbGateway Default
        {
            get
            {
                return defaultDbGateway;
            }
        }

        public static void SetDefaultDatabase(string connectionStringName) 
        {
            defaultDbGateway = new DbGateway(connectionStringName);
        }

        public static void SetDefaultDatabase(DatabaseType databaseType, string connectionString)
        {
            defaultDbGateway = new DbGateway(new Database(CreateDbProvider(databaseType, connectionString)));
        }

        public DbConnection OpenConnection() 
        {
            return this.database.CreateConnection(true);
        }

        public DbTransaction BeginTransaction(DbConnection connection) 
        {
            return this.database.BeginTransaction(connection);
        }

        public DbTransaction BeginTransaction() 
        {
            return this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public DbTransaction BeginTransaction(IsolationLevel isolationLevel) 
        {
            return this.database.BeginTransaction(isolationLevel);
        }

        public int Insert(string table, object[] values, DbTransaction transaction) 
        {
            return this.Insert(table, null, values, transaction);
        }

        public int Insert(string table, string[] columns, object[] values, DbTransaction transaction)
        {
            return this.Insert(table, null, null, values, transaction);
        }

        public int Insert(string table, string[] columns, DbType[] dbTypes, object[] values, DbTransaction transaction)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("The table name cannot be null or empty.");
            }

            if (values == null ||
                values.Length == 0)
            {
                throw new ArgumentNullException("The values of parameters cannot be null or zero.");
            }

            if (columns != null &&
                dbTypes != null &&
                !columns.Length.Equals(dbTypes.Length))
            {
                throw new ArgumentException("The length of columns of parameter don't equal the length of db types of parameters.");
            }

            if (columns != null &&
                !columns.Length.Equals(values.Length))
            {
                throw new ArgumentException("The length of columns of parameter don't equal the length of values of parameters.");
            }

            if (columns == null &&
                dbTypes != null &&
                !dbTypes.Length.Equals(values.Length))
            {
                throw new ArgumentException("The length of db types of parameter don't equal the length of values of parameters.");
            }

            ISqlStatementFactory statementFactory = this.database.DBProvider.CreateStatementFactory();

            string insertSqlStatement = statementFactory.CreateInsertStatement(table, columns);

            string[] paramNames;

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


            DbCommand insertCommand = this.PrepareSqlStringCommand(paramNames, dbTypes, values, insertSqlStatement);

            object returnValue;

            if (!this.database.IsBatchConnection && 
                !string.IsNullOrEmpty(this.database.DBProvider.SelectLastInsertedRowAutoIDStatement))
            {
                if (!this.database.DBProvider.SelectLastInsertedRowAutoIDStatement.StartsWith("SELECT SEQ_"))
                {
                    insertCommand.CommandText = insertCommand.CommandText.Trim(';') + " ; " + this.database.DBProvider.SelectLastInsertedRowAutoIDStatement;

                    if (transaction == null)
                    {
                        returnValue = this.database.ExecuteScalar(insertCommand);
                    }
                    else
                    {
                        returnValue = this.database.ExecuteScalar(insertCommand, transaction);
                    }
                }
                else
                {
                    returnValue = this.database.ExecuteScalar(CommandType.Text, 
                        string.Format(this.database.DBProvider.SelectLastInsertedRowAutoIDStatement, table));

                    if (transaction == null)
                    {
                        this.database.ExecuteNonQuery(insertCommand);
                    }
                    else
                    {
                        this.database.ExecuteNonQuery(insertCommand, transaction);
                    }
                }

                if (returnValue != null &&
                    returnValue != DBNull.Value)
                {
                    return Convert.ToInt32(returnValue);
                }
            }
            else
            {
                if (transaction == null)
                {
                    returnValue = this.database.ExecuteNonQuery(insertCommand);
                }
                else
                {
                    returnValue = this.database.ExecuteNonQuery(insertCommand, transaction);
                }

                if (returnValue != null &&
                    returnValue != DBNull.Value)
                {
                    return Convert.ToInt32(returnValue);
                }

            }

            return 0;
        }

        /// <summary>
        /// Update a table without where condition. This is a actually batch update.
        /// </summary>
        public void Update(string table,
                           string[] columns,
                           object[] values,
                           DbTransaction transaction)
        {
            this.Update(table, columns, values, null, null, transaction);
        }

        /// <summary>
        /// Update a table with where condition. 
        /// </summary>
        public void Update(string table,
                           string[] columns,
                           object[] values,
                           string where,
                           object[] whereValues,
                           DbTransaction transaction) 
        {
            this.Update(table, columns, null, values, where, null, whereValues, transaction);
        }

        /// <summary>
        /// Update a table with where condition.
        /// </summary>
        public void Update(string table, 
                           string[] columns, 
                           DbType[] dbTypes, 
                           object[] values, 
                           string where, 
                           DbType[] whereDbTypes, 
                           object[] whereValues, 
                           DbTransaction transaction) 
        {

            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("The table name cannot be null or empty.");
            }

            if (columns == null ||
                columns.Length.Equals(0))
            {
                throw new ArgumentNullException("The column names of parameters cannot be null or zero.");
            }

            if (values == null ||
                values.Length.Equals(0))
            {
                throw new ArgumentNullException("The values of parameters cannot be null or zero.");
            }

            if (!columns.Length.Equals(values.Length))
            {
                throw new ArgumentException("The length of columns should equal the length of parameter values.");
            }

            if (dbTypes != null &&
                !columns.Length.Equals(dbTypes.Length))
            {
                throw new ArgumentException("The length of columns should equal the length of parameter db types.");
            }
            
            string[] whereParamNames = this.database.GetParsedParamNames(where);

            if (whereParamNames != null &&
                whereDbTypes != null &&
                !whereParamNames.Length.Equals(whereDbTypes.Length))
            {
                throw new ArgumentException("The length of parameter db types in where sql should equal the length of parameter names in where sql if sql is not null or empty.");
            }

            if (whereParamNames != null &&
                whereValues != null &&
                !whereParamNames.Length.Equals(whereValues.Length))
            {
                throw new ArgumentException("The length of parameter names in where sql should equal the length of parameter values in where sql if sql is not null or empty.");
            }

            ISqlStatementFactory sqlStatementFactory = this.database.DBProvider.CreateStatementFactory();

            string updateSqlStatement = sqlStatementFactory.CreateUpdateStatement(table, where, columns);

            List<string> allParamNames = new List<string>();
            List<DbType> allParamDbTypes = new List<DbType>();
            List<object> allParamValues = new List<object>();

            for (int columnIndex = 0; columnIndex < columns.Length; columnIndex++) 
            {
                allParamNames.Add(columns[columnIndex]);
                if (dbTypes != null)
                {
                    allParamDbTypes.Add(dbTypes[columnIndex]);
                }
                allParamValues.Add(values[columnIndex]);
            }

            if (whereParamNames != null)
            {
                for (int whereParamIndex = 0; whereParamIndex < whereParamNames.Length; whereParamIndex++)
                {
                    allParamNames.Add(whereParamNames[whereParamIndex]);

                    if (whereDbTypes != null)
                    {
                        allParamDbTypes.Add(whereDbTypes[whereParamIndex]);
                    }

                    allParamValues.Add(whereValues[whereParamIndex]);
                }
            }

            DbCommand command = this.PrepareSqlStringCommand(allParamNames.ToArray(), 
                                                             allParamDbTypes.Count.Equals(0)? null : allParamDbTypes.ToArray() , 
                                                             allParamValues.ToArray(), 
                                                             updateSqlStatement);
            if (transaction == null)
            {
                this.database.ExecuteNonQuery(command);
            }
            else
            {
                this.database.ExecuteNonQuery(command, transaction);
            }
        }

        public void Delete(string table, DbTransaction transaction)
        {
            this.Delete(table, null, null, null, transaction);
        }

        public void Delete(string table, string where, object[] whereValues, DbTransaction transaction) 
        {
            this.Delete(table, where, null, whereValues, transaction);
        }

        public void Delete(string table, string where, DbType[] whereDbTypes, object[] whereValues, DbTransaction transaction) 
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("The table name cannot be null or empty.");
            }

            string[] whereParamNames = this.database.GetParsedParamNames(where);

            if (whereParamNames != null &&
               (whereValues == null ||
                whereValues.Length.Equals(0)))
            {
                throw new ArgumentNullException("The parameter values in where Sql cannot be null or zero if where is not null or empty.");
            }

            if (whereParamNames != null &&
                whereDbTypes != null &&
                !whereDbTypes.Length.Equals(whereParamNames.Length))
            {
                throw new ArgumentException("The length of parameter types in where Sql should equal the length of parameter names in where Sql if where is not null or empty.");
            }

            if (whereParamNames != null &&
               (whereValues == null ||
                !whereParamNames.Length.Equals(whereValues.Length)))
            {
                throw new ArgumentException("The length of parameter values in where Sql should equal the length of parameter names in where Sql if where is not null or empty..");
            }

            ISqlStatementFactory sqlStatementFactory = this.database.DBProvider.CreateStatementFactory();

            string deleteSqlStatement = sqlStatementFactory.CreateDeleteStatement(table, where);

            DbCommand command = this.PrepareSqlStringCommand(whereParamNames,
                                                             whereParamNames == null ? null : whereDbTypes,
                                                             whereParamNames == null ? null : whereValues,
                                                             deleteSqlStatement);

            if (transaction == null)
            {
                this.database.ExecuteNonQuery(command);
            }
            else
            {
                this.database.ExecuteNonQuery(command, transaction);
            }
        }

        public void ExecuteNonQuery(string sqlCommandText, object[] paramValues, DbTransaction transaction)
        {
            this.ExecuteNonQuery(sqlCommandText, null, paramValues, transaction);
        }

        public void ExecuteNonQuery(string sqlCommandText, DbType[] paramDbTypes, object[] paramValues, DbTransaction transaction) 
        {
            string[] paramNames = this.database.DiscoverParams(sqlCommandText);

            DbCommand command = this.PrepareSqlStringCommand(paramNames, paramDbTypes, paramValues, sqlCommandText);

            if (transaction == null)
            {
                this.database.ExecuteNonQuery(command);
            }
            else
            {
                this.database.ExecuteNonQuery(command, transaction);
            }
        }

        public DataSet ExecuteStoredProcedure(string storedProcedureName,
                                              string[] inParamNames,
                                              object[] inParamValues,
                                              DbTransaction transaction)
        {
            return this.ExecuteStoredProcedure(storedProcedureName, inParamNames, null, inParamValues, transaction);
        }

        public DataSet ExecuteStoredProcedure(string storedProcedureName,
                                              string[] inParamNames,
                                              DbType[] inParamDbTypes,
                                              object[] inParamValues,
                                              DbTransaction transaction)
        {
            object[] outValues = null;

            return this.ExecuteStoredProcedure(storedProcedureName, inParamNames,
                                               inParamDbTypes, inParamValues, 
                                               null, null, out outValues, transaction);
        }

        public DataSet ExecuteStoredProcedure(string storedProcedureName, 
                                              string[] inParamNames,
                                              DbType[] inParamDbTypes, 
                                              object[] inParamValues,
                                              string[] outParamNames,
                                              DbType[] outParamDbTypes,
                                              out object[] outValues,
                                              DbTransaction transaction) 
        {
            DbCommand command = this.PrepareStoredProcedureStringCommand(storedProcedureName,
                                                                         inParamNames, inParamDbTypes, inParamValues, 
                                                                         outParamNames, outParamDbTypes);

            DataSet returnDataSet;

            if (transaction == null)
            {
                returnDataSet = this.database.ExecuteDataSet(command);
            }
            else
            {
                returnDataSet = this.database.ExecuteDataSet(command, transaction);
            }

            outValues = this.GetOutDbParameterValues(outParamNames, command);

            return returnDataSet;   
        }

        #region Select IDataReader

        public IDataReader SelectReader(string table)
        {
            return this.SelectReader(table, null, null, null, null, null, null);
        }

        public IDataReader SelectReader(string table, DbTransaction transaction)
        {
            return this.SelectReader(table, null, null, null, null, null, transaction);
        }

        public IDataReader SelectReader(string table, string orderBy)
        {
            return this.SelectReader(table, null, null, null, orderBy, null);
        }

        public IDataReader SelectReader(string table, string orderBy, DbTransaction transaction)
        {
            return this.SelectReader(table, null, orderBy, transaction);
        }

        public IDataReader SelectReader(string table, string[] columns)
        {
            return this.SelectReader(table, columns, null, null, null, null, null);
        }

        public IDataReader SelectReader(string table, string[] columns, DbTransaction transaction)
        {
            return this.SelectReader(table, columns, null, transaction);
        }

        public IDataReader SelectReader(string table, string[] columns, string where, object[] whereValues)
        {
            return this.SelectReader(table, columns, where, whereValues, null, null);
        }

        public IDataReader SelectReader(string table, string[] columns, string where, object[] whereValues, DbTransaction transaction)
        {
            return this.SelectReader(table, columns, where, whereValues, null, transaction);
        }

        public IDataReader SelectReader(string table, string[] columns, string orderBy)
        {
            return this.SelectReader(table, columns, null, null, orderBy, null);
        }

        public IDataReader SelectReader(string table, string[] columns, string orderBy, DbTransaction transaction)
        {
            return this.SelectReader(table, columns, null, null, orderBy, transaction);
        }

        public IDataReader SelectReader(string table,
                                       string[] columns,
                                       string where,
                                       object[] whereValues,
                                       string orderBy)
        {
            return this.SelectReader(table, columns, where, null, whereValues, orderBy, null);
        }

        public IDataReader SelectReader(string table, 
                                        string[] columns, 
                                        string where, 
                                        object[] whereValues, 
                                        string orderBy, 
                                        DbTransaction transaction)
        {
            return this.SelectReader(table, columns, where, null, whereValues, orderBy, transaction);
        }

        public IDataReader SelectReader(string table,
                                        string[] columns,
                                        string where,
                                        DbType[] whereDbTypes,
                                        object[] whereValues,
                                        string orderBy)
        {
            return this.SelectReader(table, columns, where, whereDbTypes, whereValues, orderBy, null);
        }

        public IDataReader SelectReader(string table, 
                                        string[] columns, 
                                        string where, 
                                        DbType[] whereDbTypes, 
                                        object[] whereValues, 
                                        string orderBy, 
                                        DbTransaction transaction)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("The table name cannot be null or empty.");
            }

            string[] whereParamNames = this.database.DiscoverParams(where);

            if (whereParamNames != null &&
                whereDbTypes != null &&
                !whereParamNames.Length.Equals(whereDbTypes.Length))
            {
                throw new ArgumentException("The length of parameter types in where Sql should equal the length of parameter names in where Sql if where is not null or empty.");
            }

            if (whereParamNames != null &&
               (whereValues == null ||
               !whereParamNames.Length.Equals(whereValues.Length)))
            {
                throw new ArgumentException("The length of parameter values in where Sql should equal the length of parameter names in where Sql if where is not null or empty.");
            }

            ISqlStatementFactory sqlStatementFactory = this.database.DBProvider.CreateStatementFactory();

            string selectSqlStatement = sqlStatementFactory.CreateSelectStatement(table, where, orderBy, columns);

            DbCommand command = this.PrepareSqlStringCommand(whereParamNames, whereDbTypes, whereValues, selectSqlStatement);

            if (transaction == null)
            {
                return this.database.ExecuteReader(command);
            }
            else
            {
                return this.database.ExecuteReader(command, transaction);
            }
        }

        #endregion

        #region Select DataSet

        public DataSet SelectDataSet(string table)
        {
            return this.SelectDataSet(table, null, null, null, null, null, null);
        }

        public DataSet SelectDataSet(string table, DbTransaction transaction)
        {
            return this.SelectDataSet(table, null, null, null, null, null, transaction);
        }

        public DataSet SelectDataSet(string table, string orderBy)
        {
            return this.SelectDataSet(table, null, null, null, orderBy, null);
        }

        public DataSet SelectDataSet(string table, string orderBy, DbTransaction transaction)
        {
            return this.SelectDataSet(table, null, orderBy, transaction);
        }

        public DataSet SelectDataSet(string table, string[] columns)
        {
            return this.SelectDataSet(table, columns, null, null, null, null, null);
        }

        public DataSet SelectDataSet(string table, string[] columns, DbTransaction transaction)
        {
            return this.SelectDataSet(table, columns, null, transaction);
        }

        public DataSet SelectDataSet(string table, string[] columns, string where, object[] whereValues)
        {
            return this.SelectDataSet(table, columns, where, whereValues, null, null);
        }

        public DataSet SelectDataSet(string table, string[] columns, string where, object[] whereValues, DbTransaction transaction)
        {
            return this.SelectDataSet(table, columns, where, whereValues, null, transaction);
        }

        public DataSet SelectDataSet(string table, string[] columns, string orderBy)
        {
            return this.SelectDataSet(table, columns, null, null, orderBy, null);
        }

        public DataSet SelectDataSet(string table, string[] columns, string orderBy, DbTransaction transaction)
        {
            return this.SelectDataSet(table, columns, null, null, orderBy, transaction);
        }

        public DataSet SelectDataSet(string table,
                                     string[] columns,
                                     string where,
                                     object[] whereValues,
                                     string orderBy)
        {
            return this.SelectDataSet(table, columns, where, null, whereValues, orderBy, null);
        }

        public DataSet SelectDataSet(string table,
                                     string[] columns,
                                     string where,
                                     object[] whereValues,
                                     string orderBy,
                                     DbTransaction transaction)
        {
            return this.SelectDataSet(table, columns, where, null, whereValues, orderBy, transaction);
        }



        public DataSet SelectDataSet(string table,
                                     string[] columns,
                                     string where,
                                     DbType[] whereDbTypes,
                                     object[] whereValues,
                                     string orderBy)
        {
            return this.SelectDataSet(table, columns, where, whereDbTypes, whereValues, orderBy, null);
        }

        public DataSet SelectDataSet(string table,
                                     string[] columns,
                                     string where,
                                     DbType[] whereDbTypes,
                                     object[] whereValues,
                                     string orderBy,
                                     DbTransaction transaction)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("The table name cannot be null or empty.");
            }

            string[] whereParamNames = this.database.DiscoverParams(where);

            if (whereParamNames != null &&
                whereDbTypes != null &&
                !whereParamNames.Length.Equals(whereDbTypes.Length))
            {
                throw new ArgumentException("The length of parameter types in where Sql should equal the length of parameter names in where Sql if where is not null or empty.");
            }

            if (whereParamNames != null &&
               (whereValues == null ||
               !whereParamNames.Length.Equals(whereValues.Length)))
            {
                throw new ArgumentException("The length of parameter values in where Sql should equal the length of parameter names in where Sql if where is not null or empty.");
            }

            ISqlStatementFactory sqlStatementFactory = this.database.DBProvider.CreateStatementFactory();

            string selectSqlStatement = sqlStatementFactory.CreateSelectStatement(table, where, orderBy, columns);

            DbCommand command = this.PrepareSqlStringCommand(whereParamNames, whereDbTypes, whereValues, selectSqlStatement);

            if (transaction == null)
            {
                return this.database.ExecuteDataSet(command);
            }
            else
            {
                return this.database.ExecuteDataSet(command, transaction);
            }
        }

        #endregion

        public IDataReader ExecuteReader(string commandText)
        {
            DbCommand command = this.database.GetSqlStringCommand(commandText);

            return this.database.ExecuteReader(command);
        }

        public IDataReader ExecuteReader(string commandText, DbTransaction transaction)
        {
            DbCommand command = this.database.GetSqlStringCommand(commandText);

            return this.database.ExecuteReader(command, transaction);
        }

        public IDataReader ExecuteReader(string commandText, object[] paramValues)
        {
            return this.ExecuteReader(commandText, paramValues, null);
        }

        public IDataReader ExecuteReader(string commandText, object[] paramValues, DbTransaction transaction) 
        {
            return this.ExecuteReader(commandText, null, paramValues, transaction);
        }

        public IDataReader ExecuteReader(string commandText, DbType[] paramDbTypes, object[] paramValues)
        {
            return this.ExecuteReader(commandText, paramDbTypes, paramValues, null);
        }

        public IDataReader ExecuteReader(string commandText, DbType[] paramDbTypes, object[] paramValues, DbTransaction transaction)
        {
            string[] paramNames = this.database.DiscoverParams(commandText);

            DbCommand command = this.PrepareSqlStringCommand(paramNames, paramDbTypes, paramValues, commandText);

            if (transaction == null)
            {
                return this.database.ExecuteReader(command);
            }
            else
            {
                return this.database.ExecuteReader(command, transaction);
            }
        }

        public object ExecuteScalar(string commandText)
        {
            DbCommand command = this.database.GetSqlStringCommand(commandText);

            return this.database.ExecuteScalar(command);
        }

        public object ExecuteScalar(string commandText, DbTransaction transaction)
        {
            DbCommand command = this.database.GetSqlStringCommand(commandText);

            return this.database.ExecuteScalar(command, transaction);
        }

        public object ExecuteScalar(string commandText, object[] paramValues)
        {
            return this.ExecuteScalar(commandText, paramValues, null);
        }

        public object ExecuteScalar(string commandText, object[] paramValues, DbTransaction transaction) 
        {
            return this.ExecuteScalar(commandText, null, paramValues, transaction);
        }

        public object ExecuteScalar(string commandText, DbType[] paramDbTypes, object[] paramValues)
        {
            return this.ExecuteScalar(commandText, paramDbTypes, paramValues, null);
        }

        public object ExecuteScalar(string commandText, DbType[] paramDbTypes, object[] paramValues, DbTransaction transaction)
        {
            string[] paramNames = this.database.DiscoverParams(commandText);

            DbCommand command = this.PrepareSqlStringCommand(paramNames, paramDbTypes, paramValues, commandText);

            if (transaction == null)
            {
                return this.database.ExecuteScalar(command);
            }
            else
            {
                return this.database.ExecuteScalar(command, transaction);
            }
        }

        public DataSet ExecuteDataSet(string commandText)
        {
            DbCommand command = this.database.GetSqlStringCommand(commandText);

            return this.database.ExecuteDataSet(command);
        }

        public DataSet ExecuteDataSet(string commandText, DbTransaction transaction) 
        {
            DbCommand command = this.database.GetSqlStringCommand(commandText);

            return this.database.ExecuteDataSet(command, transaction);
        }

        public DataSet ExecuteDataSet(string commandText, object[] paramValues) 
        {
            return this.ExecuteDataSet(commandText, paramValues, null);
        }

        public DataSet ExecuteDataSet(string commandText, object[] paramValues, DbTransaction transaction)
        {
            return this.ExecuteDataSet(commandText, null, paramValues, transaction);
        }

        public DataSet ExecuteDataSet(string commandText, DbType[] paramDbTypes, object[] paramValues)
        {
            return this.ExecuteDataSet(commandText, paramDbTypes, paramValues);
        }

        public DataSet ExecuteDataSet(string commandText, DbType[] paramDbTypes, object[] paramValues, DbTransaction transaction)
        {
            string[] paramNames = this.database.DiscoverParams(commandText);

            DbCommand command = this.PrepareSqlStringCommand(paramNames, paramDbTypes, paramValues, commandText);

            if (transaction == null)
            {
                return this.database.ExecuteDataSet(command);
            }
            else
            {
                return this.database.ExecuteDataSet(command, transaction);
            }
        }

        public void CloseConnection(DbConnection connection) 
        {
            this.database.CloseConnection(connection);
        }

        public void CloseConnection(DbTransaction transaction)
        {
            this.database.CloseConnection(transaction);
        }

        #endregion
    }
}
