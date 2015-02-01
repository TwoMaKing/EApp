using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace EApp.Data
{
    public abstract class DbProvider
    {
        #region "Protected Members"

        protected System.Data.Common.DbProviderFactory dbProviderFactory;

        protected System.Data.Common.DbConnectionStringBuilder dbConnectionStringBuilder;

        protected DbProvider(string connectionString, System.Data.Common.DbProviderFactory dbProviderFactory)
        {
            this.dbConnectionStringBuilder = new DbConnectionStringBuilder();
            this.dbConnectionStringBuilder.ConnectionString = connectionString;
            this.dbProviderFactory = dbProviderFactory;

        }

        #endregion

        #region "Properties"

        public string ConnectionString
        {
            get { return this.dbConnectionStringBuilder.ConnectionString; }
        }

        public System.Data.Common.DbProviderFactory DbProviderFactory
        {
            get { return this.dbProviderFactory; }
        }

        #endregion

        #region "Abstract Memebers for further ORM"

        /// <summary>
        /// Adjust SQL Parameter db type.
        /// </summary>
        public abstract void AdjustParameter(DbParameter param);

        /// <summary>
        ///Creates the SQL statement factory.
        ///</summary>
        /// <returns></returns>
        public abstract ISqlStatementFactory CreateStatementFactory();

        /// <summary>
        /// Discovers params from SQL text.
        /// E.g. insert into [user] values (@user_name, @user_email, @user_password). 
        /// Having 3 parameters: @user_name, @user_email, @user_password.
        /// </summary>
        /// <param name="sql">The full or part of SQL text.</param>
        /// <returns>The discovered params.</returns>
        public abstract string[] DiscoverParams(string sql);

        /// <summary>
        /// Builds the name of the parameter. 
        /// E.g. for MS SQL add a '@' char at the begin with the column name. e.g. @user_name, @user_email
        /// insert into [user] values (@user_name, @user_email)
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public abstract string BuildParameterName(string name);

        /// <summary>
        /// Builds the name of the column or table.
        /// E.g. for MS SQL add '[' and ']' at the left and right of the column name.
        /// e.g. [user_name], [user_email]
        /// update [user] set [user_name]=@user_name, [user_email]=@user_email where [user_id]=@user_id
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public abstract string BuildColumnName(string name);

        /// <summary>
        /// Gets the select sql script for the last inserted row auto ID statement.
        /// </summary>
        /// <value>The select last inserted row auto ID statement.</value>
        public abstract string SelectLastInsertedRowAutoIDStatement { get; }

        ///<summary>
        ///Gets the param prefix.
        /// </summary>
        /// <value>The param prefix.</value>
        public abstract string ParamPrefix { get; }

        #endregion

    }


}

