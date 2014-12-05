using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace EApp.Common.DataAccess
{
    public enum DataBaseType
    {
        /// <summary>
        /// Common SqlServer, including SQL Server 7.X, 8.X and 9.X
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
        /// 
        /// </summary>
        Other = 3
    }


    public class DbGateway
    {
        private Database database;

        private static object lockObject = new object();

        public DbGateway(): this(DbProviderFactory.Default){}

        public DbGateway(DbProvider dbProvider) 
        { 
            this.database = new Database(dbProvider);
        }

        private DbCommand PrepareSqlStringCommand(string[] paramColumns, DbType[] paramDbTypes, object[] paramValues, string sqlCommandText) 
        {
            if (paramColumns == null ||
                paramColumns.Length == 0)
            {
                throw new ArgumentNullException("The column names of parameters cannot be null or zero.");
            }

            if (paramValues == null ||
                paramValues.Length == 0)
            {
                throw new ArgumentNullException("The values of parameters cannot be null or zero.");
            }

            if (paramDbTypes != null &&
                !paramColumns.Length.Equals(paramDbTypes.Length))
            {
                throw new ArgumentException("The length of columns of parameter don't equal the length of db types of parameters.");
            }

            if (!paramColumns.Length.Equals(paramValues.Length))
            {
                throw new ArgumentException("The length of columns of parameter don't equal the length of values of parameters.");
            }

            if (paramDbTypes != null &&
                !paramDbTypes.Length.Equals(paramValues.Length))
            {
                throw new ArgumentException("The length of db types of parameter don't equal the length of values of parameters.");
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
                    this.database.ParameterCache.CreateAndCacheParameters(sqlCommandText, command, paramColumns, paramDbTypes, paramValues);
                }
            }

            return command;
        }

        private void PrepareStoredProcedureStringCommand(string procedureName, string[] paramColumnNames, DbType[] paramTypes, object[] paramValues, string[] outParamNames, DbType[] outParamTypes)
        {

        }

        public int Insert(string table, string[] paramColumns, DbType[] paramDbTypes, object[] paramValues, DbTransaction transaction)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("The table name cannot be null or zero.");
            }

            if (paramColumns == null ||
                paramColumns.Length == 0)
            {
                throw new ArgumentNullException("The column names of parameters cannot be null or zero.");
            }

            if (paramValues == null ||
                paramValues.Length == 0)
            {
                throw new ArgumentNullException("The values of parameters cannot be null or zero.");
            }

            if (paramDbTypes != null &&
                !paramColumns.Length.Equals(paramDbTypes.Length))
            {
                throw new ArgumentException("The length of columns of parameter don't equal the length of db types of parameters.");
            }

            if (!paramColumns.Length.Equals(paramValues.Length))
            {
                throw new ArgumentException("The length of columns of parameter don't equal the length of values of parameters.");
            }

            if (paramDbTypes != null &&
                !paramDbTypes.Length.Equals(paramValues.Length))
            {
                throw new ArgumentException("The length of db types of parameter don't equal the length of values of parameters.");
            }

            ISqlStatementFactory statementFactory = this.database.DBProvider.CreateStatementFactory();

            string insertSqlStatement = statementFactory.CreateInsertStatement(table, paramColumns);

            DbCommand insertCommand = PrepareSqlStringCommand(paramColumns, paramDbTypes, paramValues, insertSqlStatement);

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

        public void Update(string table, 
                           string[] paramColumns, DbType[] paramDbTypes, object[] paramValues, 
                           string where, DbType[] whereDbTypes, object[] whereValues, 
                           DbTransaction transaction) 
        { 

            
        }

        public void Delete(string table, string where, DbType[] whereDbTypes, object[] whereValues, DbTransaction transaction) 
        {
        
        }

        public void ExecuteStoredProcedure() 
        {
        
        
        }

        public DbDataReader ExecuteReader(string commandText, string[] paramColumns, DbType[] paramDbTypes, object[] paramValues)
        {
            return null;
        }

        public object ExecuteScalar(string commandText, string[] paramColumns, DbType[] paramDbTypes, object[] paramValues)
        {
            return null;
        }

        public DataSet LoadDataSet(string commandText, string[] paramColumns, DbType[] paramDbTypes, object[] paramValues)
        {
            return null;
        }

    }
}
