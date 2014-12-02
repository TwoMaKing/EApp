using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;


namespace EApp.Common.DataAccess
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
        /// Adjust SQL Parameter
        /// </summary>
        public abstract void AdjustParameter(DbParameter param);

        /// <summary>
        ///Creates the SQL statement factory.
        ///</summary>
        /// <returns></returns>
        public abstract IStatementFactory CreateStatementFactory();


        /// <summary>
        ///Discovers params from SQL text.
        /// </summary>
        /// <param name="sql">The full or part of SQL text.</param>
        /// <returns>The discovered params.</returns>
        public abstract string[] DiscoverParams(string sql);


        /// <summary>
        /// Builds the name of the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public abstract string BuildParameterName(string name);

        /// <summary>
        /// Builds the name of the column.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public abstract string BuildColumnName(string name);

        /// <summary>
        /// Gets the select last inserted row auto ID statement.
        /// </summary>
        /// <value>The select last inserted row auto ID statement.</value>
        public abstract string SelectLastInsertedRowAutoIDStatement { get; }


        ///<summary>
        ///Gets the param prefix.
        /// </summary>
        /// <value>The param prefix.</value>
        public abstract string ParamPrefix { get; }

        /// <summary>
        /// Gets the left token of table name or column name.
        /// </summary>
        /// <value>The left token.</value>
        public abstract string LeftToken { get; }

        /// <summary>
        /// Gets the right token of table name or column name.
        /// </summary>
        /// <value>The right token.</value>
        public abstract string RightToken { get; }


        #endregion

    }


}

