using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace EApp.Data
{
    public sealed class Database
    {
        #region "cache"

        private DbParameterCache parameterCache;

        private Dictionary<string, string[]> parameterNameCache = new Dictionary<string, string[]>();

        internal DbParameterCache ParameterCache
        {
            get 
            { 
                return this.parameterCache; 
            }
        }

        internal Dictionary<string, string[]> ParameterNameCache
        {
            get 
            { 
                return this.parameterNameCache; 
            }
        }

        internal string[] GetParsedParamNames(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            string[] paramNames = null;

            lock (parameterNameCache)
            {
                if (this.parameterNameCache.ContainsKey(sql))
                {
                    paramNames = this.parameterNameCache[sql];
                }
                else
                {
                    paramNames = this.DiscoverParams(sql);
                    this.parameterNameCache.Add(sql, paramNames);
                }
            }

            return paramNames;
        }


        #endregion

        #region "static"

        public static Database Default;

        static Database()
        {
            if (DbProviderFactory.Default == null)
            {
                Default = null;
            }
            else
            {
                Default = new Database(DbProviderFactory.Default);
            }
        }

        #endregion

        #region "Constructors"

        public Database(DbProvider dbProvider)
            : this()
        {
            this.dbProvider = dbProvider;
        }

        public Database()
        {
            this.parameterCache = new DbParameterCache(this);
        }

        #endregion

        #region "Helper Methods"

        public string[] DiscoverParams(string sql)
        {
            return DBProvider.DiscoverParams(sql);
        }

        #endregion

        #region "private members"

        private DbProvider dbProvider;

        private DbCommand CreateCommandByCommandType(CommandType commandType, string commandText)
        {
            DbCommand command = this.dbProvider.DbProviderFactory.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }

        private void DoLoadDataSet(DbCommand command, DataSet ds, string[] tableNames)
        {
            using (DbDataAdapter adapter = this.GetDataAdapter())
            {
                (adapter as IDbDataAdapter).SelectCommand = command;

                try
                {
                    string systemCreatedTableNameRoot = "table";
                    for (int i = 0; i <= tableNames.Length - 1; i++)
                    {
                        string systemCreatedTableName = (i == 0 ? systemCreatedTableNameRoot : systemCreatedTableNameRoot + i.ToString());
                        adapter.TableMappings.Add(systemCreatedTableName, tableNames[i]);
                    }
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private object DoExecuteScalar(DbCommand command)
        {
            try
            {
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection(command);
            }
        }

        private int DoExecuteNonQuery(DbCommand command)
        {
            if (this.IsBatchConnection)
            {
                return 0;
            }

            try
            {
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection(command);
            }
        }

        private IDataReader DoExecuteReader(DbCommand command, CommandBehavior cmdBehavior)
        {
            try
            {
                IDataReader reader = command.ExecuteReader(cmdBehavior);
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmdBehavior != CommandBehavior.CloseConnection)
                {
                    this.CloseConnection(command);
                }
            }
        }

        private int DoUpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand)
        {
            int rows = 0;

            using (DbDataAdapter adpter = this.GetDataAdapter())
            {
                IDbDataAdapter explicitAdapter = (IDbDataAdapter)adpter;

                if (insertCommand != null)
                {
                    explicitAdapter.InsertCommand = insertCommand;
                }
                if (updateCommand != null)
                {
                    explicitAdapter.UpdateCommand = updateCommand;
                }
                if (deleteCommand != null)
                {
                    explicitAdapter.DeleteCommand = deleteCommand;
                }
            
                try
                {
                    rows = adpter.Update(dataSet.Tables[tableName]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rows;
        }

        private static void PrepareCommand(DbCommand command, DbConnection connection)
        {
            command.Connection = connection;
        }

        private static void PrepareCommand(DbCommand command, DbTransaction Transaction)
        {
            PrepareCommand(command, Transaction.Connection);
            command.Transaction = Transaction;
        }

        private static void ConfigureParameter(DbParameter param, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.DbType = dbType;
            param.Size = size;
            param.Value = (value == null ? DBNull.Value : value);
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
        }

        private DbParameter CreateParameter(string name, DbType dbtype, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter param = this.CreateParameter(name);
            ConfigureParameter(param, name, dbtype, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            return param;
        }

        private DbParameter CreateParameter(string name)
        {
            DbParameter param = this.dbProvider.DbProviderFactory.CreateParameter();
            param.ParameterName = this.dbProvider.BuildParameterName(name);
            return param;
        }

        #endregion

        #region "public members"


        public string ConnectionString
        {
            get { return this.dbProvider.ConnectionString; }
        }

        public DbProvider DBProvider
        {
            get { return this.dbProvider; }
        }

        #endregion

        #region "DataBaseFactoryMethod"

        public DbCommand GetStoredProcCommand(string storedProcedureName)
        {
            return this.CreateCommandByCommandType(CommandType.StoredProcedure, storedProcedureName);
        }

        public DbCommand GetSqlStringCommand(string query)
        {
            return this.CreateCommandByCommandType(CommandType.Text, query);
        }

        public DbDataAdapter GetDataAdapter()
        {
            return this.dbProvider.DbProviderFactory.CreateDataAdapter();
        }

        public ISqlStatementFactory GetStatementFactory()
        {
            return this.dbProvider.CreateStatementFactory();
        }

        #endregion

        #region "Close Connection"

        public void CloseConnection(DbCommand command)
        {
            if (command != null & command.Connection.State != ConnectionState.Closed & this.batchConnection == null)
            {   
                if (command.Transaction == null)
                {
                    CloseConnection(command.Connection);
                    command.Dispose();
                }
            }
        }

        public void CloseConnection(DbConnection conn)
        {
            if (conn != null & conn.State != ConnectionState.Closed)
            {
                try
                {
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void CloseConnection(DbTransaction tran)
        {
            if (tran != null & 
                tran.Connection != null &&
                tran.Connection.State != ConnectionState.Closed)
            {
                CloseConnection(tran.Connection);
                tran.Dispose();
            }
        }

        #endregion

        #region "Open Connection"

        public DbConnection GetConnection()
        {
            if (this.batchConnection == null)
            {
                return CreateConnection();
            }
            else
            {
                return batchConnection;
            }
        }

        public DbConnection GetConnection(bool tryOpenning)
        {
            if (this.batchConnection == null)
            {
                return CreateConnection(tryOpenning);
            }
            else
            {
                return this.batchConnection;
            }
        }

        public DbConnection CreateConnection()
        {
            DbConnection newConnection = this.dbProvider.DbProviderFactory.CreateConnection();
            newConnection.ConnectionString = ConnectionString;
            return newConnection;
        }

        public DbConnection CreateConnection(bool tryOpenning)
        {
            if (!tryOpenning)
            {
                return this.CreateConnection();
            }

            DbConnection connection = null;
            try
            {
                connection = this.CreateConnection();
                connection.Open();
            }
            catch
            {
                try
                {
                    connection.Close();

                }
                catch
                {
                }
            }
            return connection;
        }

        #endregion

        #region "Load Execute Member"

        #region "LoadDataSet"

        public void LoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames)
        {
            using (DbConnection connection = this.GetConnection())
            {
                Database.PrepareCommand(command, connection);
                this.DoLoadDataSet(command, dataSet, tableNames);
            }
        }

        public void LoadDataSet(DbCommand command, DataSet dataSet, string tableName)
        {
            LoadDataSet(command, dataSet, new string[] { tableName });
        }

        public void LoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            this.DoLoadDataSet(command, dataSet, tableNames);
        }

        public void LoadDataSet(DbCommand command, DataSet dataSet, string tableName, DbTransaction transaction)
        {
            LoadDataSet(command, dataSet, new string[] { tableName }, transaction);
        }

        public void LoadDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                LoadDataSet(command, dataSet, tableNames);
            }

        }

        public void LoadDataSet(CommandType commandType, string commandText, DataSet dataSet, string tableName)
        {
            this.LoadDataSet(commandType, commandText, dataSet, new string[] { tableName });
        }


        public void LoadDataSet(DbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                LoadDataSet(command, dataSet, tableNames, transaction);
            }
        }

        public void LoadDataSet(DbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string tableName)
        {
            this.LoadDataSet(transaction, commandType, commandText, dataSet, new string[] { tableName });
        }

        public DataSet ExecuteDataSet(DbCommand command)
        {
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.InvariantCulture;
            this.LoadDataSet(command, ds, "Table");
            return ds;
        }

        public DataSet ExecuteDataSet(DbCommand command, DbTransaction transaction)
        {
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.InvariantCulture;
            this.LoadDataSet(command, ds, "Table", transaction);
            return ds;
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.InvariantCulture;
            this.LoadDataSet(commandType, commandText, ds, "Table");
            return ds;
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText, DbTransaction transaction)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteDataSet(command, transaction);
            }
        }

        #endregion

        #region "ExecuteReader"

        public IDataReader ExecuteReader(DbCommand command)
        {
            DbConnection connection = this.GetConnection(true);
            PrepareCommand(command, connection);
            try
            {
                return DoExecuteReader(command, CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                try
                {
                    CloseConnection(connection);
                }
                catch
                {

                }

                throw;
            }
        }

        public IDataReader ExecuteReader(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return DoExecuteReader(command, CommandBehavior.Default);
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteReader(command);
            }
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText, DbTransaction transaction)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteReader(command, transaction);
            }
        }

        #endregion

        #region "ExecuteScalar"

        public object ExecuteScalar(DbCommand command)
        {
            using (DbConnection connection = this.GetConnection(true))
            {
                PrepareCommand(command, connection);
                return this.DoExecuteScalar(command);
            }
        }

        public object ExecuteScalar(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return this.DoExecuteScalar(command);
        }

        public object ExecuteScalar(CommandType commandtype, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandtype, commandText))
            {
                return this.ExecuteScalar(command);
            }
        }

        public object ExecuteScalar(CommandType commandType, string commandtext, DbTransaction transaction)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandtext))
            {
                PrepareCommand(command, transaction);
                return this.ExecuteScalar(command, transaction);
            }
        }

        #endregion

        #region "ExecuteNonQuery"

        public int ExecuteNonQuery(DbCommand command)
        {
            using (DbConnection connection = this.GetConnection(true))
            {
                PrepareCommand(command, connection);
                return this.DoExecuteNonQuery(command);
            }
        }

        public int ExecuteNonQuery(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return this.DoExecuteNonQuery(command);
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteNonQuery(command);
            }
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText, DbTransaction transaction)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteNonQuery(command, transaction);
            }
        }

        #endregion

        #region "UpdataSet"

        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, DbTransaction transaction)
        {
            if (insertCommand != null)
            {
                PrepareCommand(insertCommand, transaction);
            }
            if (updateCommand != null)
            {
                PrepareCommand(updateCommand, transaction);
            }
            if (deleteCommand != null)
            {
                PrepareCommand(deleteCommand, transaction);
            }

            return this.DoUpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand);
        }

        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand)
        {
            int rows = 0;

            using (DbConnection connection = this.GetConnection(true))
            {
                if (insertCommand != null)
                {
                    PrepareCommand(insertCommand, connection);
                }
                
                if (updateCommand != null)
                {
                    PrepareCommand(updateCommand, connection);
                }

                if (deleteCommand != null)
                {
                    PrepareCommand(deleteCommand, connection);
                }

                rows = this.DoUpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand);
            }

            return rows;
        }

        #endregion

        #region "Transactions"

        public DbTransaction BeginTransaction()
        {
            return this.GetConnection(true).BeginTransaction();
        }

        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return this.GetConnection(true).BeginTransaction(isolationLevel);
        }

        public DbTransaction BeginTransaction(DbConnection connection)
        {
            return connection.BeginTransaction();
        }

        public DbTransaction BeginTransaction(DbConnection connection, IsolationLevel isolationLevel)
        {
            return connection.BeginTransaction(isolationLevel);
        }

        public void RollbackTransaction(DbTransaction transaction)
        {
            transaction.Rollback();
        }

        public void CommitTransaction(DbTransaction transaction)
        {
            transaction.Commit();
        }

        #endregion

        #region "DbCommand Parameter Methods"

        public void AddParameter(DbCommand command, string name, DbType dbtype, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion,
        object value)
        {
            DbParameter parameter = this.CreateParameter(name, dbtype, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            this.DBProvider.AdjustParameter(parameter);
            command.Parameters.Add(parameter);
        }

        public void AddParameter(DbCommand command, string name, DbType dbtype, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            AddParameter(command, name, dbtype, 0, direction, false, 0, 0, sourceColumn, sourceVersion,
            value);
        }

        public void AddOutParameter(DbCommand command, string name, DbType dbType, int size)
        {
            AddParameter(command, name, dbType, size, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default,
            DBNull.Value);
        }

        public void AddInParameter(DbCommand command, string name, DbType dbType)
        {
            AddParameter(command, name, dbType, ParameterDirection.Input, string.Empty, DataRowVersion.Default, null);
        }

        public void AddInParameter(DbCommand command, string name, DbType dbType, object value)
        {
            AddParameter(command, name, dbType, ParameterDirection.Input, string.Empty, DataRowVersion.Default, value);
        }

        public void AddInParameter(DbCommand command, string name, object value)
        {
            AddParameter(command, name, DbType.Object, ParameterDirection.Input, string.Empty, DataRowVersion.Default, value);
        }

        public void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            AddParameter(command, name, dbType, 0, ParameterDirection.Input, true, 0, 0, sourceColumn, sourceVersion,
            null);
        }


        #endregion

        #endregion

        #region "Batch Database"

        private DbConnection batchConnection = null;

        private int batchSize = 1;

        private BatchCommander batchCommander = null;
        
        public bool IsBatchConnection
        {
            get { return batchConnection != null; }
        }

        #endregion
    }

}

