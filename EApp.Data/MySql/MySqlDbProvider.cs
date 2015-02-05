using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EApp.Common.Serialization;
using EApp.Data.Queries;
using MySql.Data.MySqlClient;

namespace EApp.Data.MySql
{
    public class MySqlDbProvider : DbProvider
    {
        private const char Parameter_Prefix = '?';

        private ISqlStatementFactory sqlStatementFactory = new MySqlStatementFactory();

        public MySqlDbProvider(string connectionString) : 
            base(connectionString, MySqlClientFactory.Instance) 
        { 
        
        }

        /// <summary>
        /// Adjust common Parameter db type to the specified MySql Parameter db type.
        /// </summary>
        public override void AdjustParameter(DbParameter param)
        {
            MySqlParameter mySqlParam = (MySqlParameter)param;

            object value = param.Value;
            DbType type = param.DbType;

            if (value == null || value == DBNull.Value)
            {
                mySqlParam.Value = DBNull.Value;
                if (mySqlParam.DbType != DbType.Binary && mySqlParam.DbType != DbType.Int32)
                {
                    mySqlParam.MySqlDbType = MySqlDbType.VarChar;
                }

                return;
            }

            if (value.GetType().IsEnum)
            {
                mySqlParam.MySqlDbType = MySqlDbType.Enum;

                return;
            }

            if (value.GetType() == typeof(byte[]))
            {
                mySqlParam.MySqlDbType = MySqlDbType.VarBinary;
                return;
            }

            if (value.GetType() == typeof(Guid))
            {
                mySqlParam.MySqlDbType = MySqlDbType.VarChar;
                mySqlParam.Value = value.ToString();
                return;
            }

            if (value.GetType() == typeof(Byte) || value.GetType() == typeof(SByte) ||
                value.GetType() == typeof(Int16) || value.GetType() == typeof(Int32) ||
                value.GetType() == typeof(Int64) || value.GetType() == typeof(UInt16) ||
                value.GetType() == typeof(UInt32) || value.GetType() == typeof(UInt64))
            {
                mySqlParam.MySqlDbType = MySqlDbType.Int32;
                return;
            }

            if (value.GetType() == typeof(Single) || value.GetType() == typeof(Double))
            {
                mySqlParam.MySqlDbType = MySqlDbType.Float;
                return;
            }

            if (value.GetType() == typeof(Boolean))
            {
                mySqlParam.MySqlDbType = MySqlDbType.Bit;
                mySqlParam.Value = (((bool)value) ? 1 : 0);
                return;
            }

            if (value.GetType() == typeof(Char))
            {
                mySqlParam.MySqlDbType = MySqlDbType.VarChar;
                return;
            }

            if (value.GetType() == typeof(Decimal))
            {
                mySqlParam.MySqlDbType = MySqlDbType.Decimal;
                return;
            }

            //datetime is special here
            if (value.GetType() == typeof(DateTime) || type.Equals(DbType.DateTime) ||
                type.Equals(DbType.Date) || type.Equals(DbType.Time))
            {
                mySqlParam.MySqlDbType = MySqlDbType.Datetime;
                mySqlParam.Value = value;

                return;
            }

            if (value.GetType() == typeof(string))
            {
                mySqlParam.MySqlDbType = MySqlDbType.VarChar;
                if (value.ToString().Length > 2000)
                {
                    mySqlParam.MySqlDbType = MySqlDbType.Text;
                }
                return;
            }

            //by default, threat as string and then Serialize it a string.
            mySqlParam.MySqlDbType = MySqlDbType.Text;

            mySqlParam.Value = SerializationManager.Serialize(mySqlParam.Value);
        }

        public override ISqlStatementFactory CreateStatementFactory()
        {
            return this.sqlStatementFactory;
        }

        /// <summary>
        /// Discovers params from SQL text.
        /// E.g. insert into `user` values (?user_name, ?user_email, ?user_password)
        /// Having 3 parameters: ?user_name, ?user_email, ?user_password.
        /// </summary>
        /// <param name="sql">The full or part of SQL text.</param>
        /// <returns>The discovered params.</returns>
        public override string[] DiscoverParams(string sql)
        {
            if (sql == null)
            {
                return null;
            }

            Regex r = new Regex("\\" + this.ParamPrefix + @"([\w\d_]+)");

            MatchCollection ms = r.Matches(sql);

            if (ms.Count == 0)
            {
                return null;
            }

            string[] paramNames = new string[ms.Count];
            for (int i = 0; i < ms.Count; i++)
            {
                paramNames[i] = ms[i].Value;
            }
            return paramNames;
        }

        /// <summary>
        /// Builds the name of the parameter. 
        /// E.g. for MySql add a '?' char at the begin with the columns. e.g. ?user_name, ?user_email
        /// insert into [user] values (?user_name, ?user_email)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string BuildParameterName(string name)
        {
            name = name.Trim('`');

            if (!name[0].Equals(Parameter_Prefix))
            {
                return name.Insert(0, this.ParamPrefix);
            }

            return name;
        }

        /// <summary>
        /// Builds the name of the column or table.
        /// E.g. for MySql add '`' and '`' at the left and right of the column name.
        /// e.g. `user_name`, `user_email`
        /// update `user` set `user_name`=?user_name, `user_email`=?user_email where `user_id`=?user_id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string BuildColumnName(string name)
        {
            if (!name.StartsWith("`")) 
            {
                name = name.Insert(0, "`");
            }

            if (!name.EndsWith("`"))
            {
                name = name + "`";
            }

            return name;
        }

        public override string SelectLastInsertedRowAutoIDStatement
        {
            get 
            { 
                return "SELECT LAST_INSERT_ID()"; 
            }
        }

        public override string ParamPrefix
        {
            get 
            { 
                return Parameter_Prefix.ToString(); 
            }
        }

        public override WhereClauseBuilder<T> CreateWhereClauseBuilder<T>()
        {
            return new MySqlWhereClauseBuilder<T>(new XmlObjectMappingResolver(""));
        }

    }
}
