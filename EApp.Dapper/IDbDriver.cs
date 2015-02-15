using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public interface IDbDriver
    {
        /// <summary>
        /// Adjust SQL Parameter db type.
        /// </summary>
        void AdjustParameter(DbParameter parameter);

        /// <summary>
        ///Creates the SQL statement factory.
        ///</summary>
        /// <returns></returns>
        ISqlStatementFactory CreateStatementFactory();

        /// <summary>
        /// Discovers params from SQL text.
        /// E.g. insert into [user] values (@user_name, @user_email, @user_password). 
        /// Having 3 parameters: @user_name, @user_email, @user_password.
        /// </summary>
        /// <param name="sql">The full or part of SQL text.</param>
        /// <returns>The discovered params.</returns>
        string[] DiscoverParams(string sql);

        /// <summary>
        /// Builds the name of the parameter. 
        /// E.g. for MS SQL add a '@' char at the begin with the column name. e.g. @user_name, @user_email
        /// insert into [user] values (@user_name, @user_email)
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string BuildParameterName(string name);

        /// <summary>
        /// Builds the name of the column or table.
        /// E.g. for MS SQL add '[' and ']' at the left and right of the column name.
        /// e.g. [user_name], [user_email]
        /// update [user] set [user_name]=@user_name, [user_email]=@user_email where [user_id]=@user_id
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string BuildColumnName(string name);

        /// <summary>
        /// Gets the select sql script for the last inserted row auto ID statement.
        /// </summary>
        /// <value>The select last inserted row auto ID statement.</value>
        string SelectLastInsertedRowAutoIDStatement { get; }

        ///<summary>
        ///Gets the param prefix.
        /// </summary>
        /// <value>The param prefix.</value>
        char ParameterPrefix { get; }

        /// <summary>
        /// Gets the left token of parameter name. e.g. [, i.e. [post_id]
        /// </summary>
        char ParameterLeftToken { get; }

        /// <summary>
        /// Gets the right token of parameter name. e.g.], i.e. [post_id]
        /// </summary>
        char ParameterRightToken { get; }

        /// <summary>
        /// Get the wild char token. e.g. '%'
        /// </summary>
        char WildCharToken { get; }

        /// <summary>
        /// Get the wildsingle char token. e.g. '_'
        /// </summary>
        char WildSingleCharToken { get; }
    }
}
