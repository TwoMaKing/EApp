using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace EApp.Repositories.SQL
{
    public interface ISQLRepositoryContextDbProvider
    {
        DbTransaction DbTransaction { get; }

        IEnumerable<DbCommand> DbCommands { get; }

        void Insert(string table, string[] columns, object[] values);
        
        void Insert(string table, object[] values);

        void Update(string table,
                    string[] columns,
                    object[] values,
                    string whereSql,
                    object[] whereParamValues);

        void Delete(string table, string whereSql, object[] whereParamValues);

        void AddDbCommand(DbCommand dbCommand, string[] paramNames = null, object[] paramValues = null);

        string BuildParameterName(string paramName);
    }
}
