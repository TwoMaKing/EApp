using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EApp.Common.Serialization;

namespace EApp.Data.SqlServer
{
    public class SqlServerDbProvider : DbProvider
    {
        private const char Parameter_Prefix = '@';

        private SqlServerStatementFactory sqlServerStatementFactory = new SqlServerStatementFactory();

        public SqlServerDbProvider(string connectionString) : 
            base(connectionString, System.Data.SqlClient.SqlClientFactory.Instance)
        {

        }

        /// <summary>
        /// Adjust common Parameter db type to the specified MS SQL Server Parameter db type.
        /// </summary>
        public override void AdjustParameter(DbParameter param)
        {
            SqlParameter sqlParam = (SqlParameter)param;

            object value = param.Value;
            
            DbType type = param.DbType;

            if (value == null || value == DBNull.Value)
            {
                sqlParam.Value = DBNull.Value;
                if (sqlParam.DbType != DbType.Binary && sqlParam.DbType != DbType.Int32)
                {
                    sqlParam.SqlDbType = SqlDbType.NVarChar;
                }
                return;
            }

            if (value.GetType() == typeof(byte[]))
            {   
                sqlParam.SqlDbType = SqlDbType.Image;
                return;
            }

            if (value.GetType().IsEnum)
            {
                sqlParam.SqlDbType = SqlDbType.Int;
                return;
            }

            if (value.GetType() == typeof(Guid))
            {
                sqlParam.SqlDbType = SqlDbType.UniqueIdentifier;
                return;
            }

            if (value.GetType() == typeof(Byte) || value.GetType() == typeof(SByte) ||
                value.GetType() == typeof(Int16) || value.GetType() == typeof(Int32) ||
                value.GetType() == typeof(Int64) || value.GetType() == typeof(UInt16) ||
                value.GetType() == typeof(UInt32) || value.GetType() == typeof(UInt64))
            {
                sqlParam.SqlDbType = SqlDbType.Int;
                return;
            }

            if (value.GetType() == typeof(Single) || value.GetType() == typeof(Double))
            {
                sqlParam.SqlDbType = SqlDbType.Float;
                return;
            }

            if (value.GetType() == typeof(Boolean))
            {
                sqlParam.SqlDbType = SqlDbType.Bit;
                sqlParam.Value = (((bool)value) ? 1 : 0);
                return;
            }

            if (value.GetType() == typeof(Char))
            {
                sqlParam.SqlDbType = SqlDbType.NChar;
                return;
            }

            if (value.GetType() == typeof(Decimal))
            {
                sqlParam.SqlDbType = SqlDbType.Decimal;
                return;
            }

            if (value.GetType() == typeof(DateTime))
            {
                sqlParam.SqlDbType = SqlDbType.DateTime;
                return;
            }

            if (value.GetType() == typeof(string))
            {
                sqlParam.SqlDbType = SqlDbType.NVarChar;
                if (value.ToString().Length > 2000)
                {
                    sqlParam.SqlDbType = SqlDbType.Text;
                }
                return;
            }

            //by default, threat as string and then Serialize it a string.
            sqlParam.SqlDbType = SqlDbType.NText;
            sqlParam.Value = SerializationManager.Serialize(sqlParam.Value);
        }

        public override ISqlStatementFactory CreateStatementFactory()
        {
            return this.sqlServerStatementFactory;
        }

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

        public override string BuildParameterName(string name)
        {
            name = name.Trim('[', ']');

            if (!name[0].Equals(Parameter_Prefix))
            {
                return name.Insert(0, this.ParamPrefix);
            }

            return name;
        }

        public override string BuildColumnName(string name)
        {
            if (!name.StartsWith("["))
            {
                name = name.Insert(0, "[");
            }

            if (!name.EndsWith("]"))
            {
                name = name + "]";
            }

            return name;
        }

        public override string SelectLastInsertedRowAutoIDStatement
        {
            get 
            { 
                return "SELECT SCOPE_IDENTITY()"; 
            }
        }

        public override string ParamPrefix
        {
            get 
            {
                return Parameter_Prefix.ToString(); 
            }
        }

    }
}
