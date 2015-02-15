using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EApp.Dapper
{
    public abstract class DbDriver : IDbDriver
    {
        public abstract void AdjustParameter(DbParameter parameter);

        public abstract ISqlStatementFactory CreateStatementFactory();

        public string[] DiscoverParams(string sql)
        {
            if (sql == null)
            {
                return null;
            }

            Regex r = new Regex("\\" + this.ParameterPrefix + @"([\w\d_]+)");
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

        public string BuildParameterName(string name)
        {
            string newParameterName = name.Trim('[', ']', '`', '\"');

            string parameterPrefixString = this.ParameterPrefix.ToString();

            if (!newParameterName.StartsWith(parameterPrefixString))
            {
                return newParameterName.Insert(0, parameterPrefixString);
            }

            return newParameterName;
        }

        public string BuildColumnName(string name)
        {
            string newColumnName = name.Trim('[', ']', '`', '\"');

            newColumnName.Insert(0, this.ParameterLeftToken.ToString());
            newColumnName += this.ParameterLeftToken;

            return newColumnName;
        }

        public abstract string SelectLastInsertedRowAutoIDStatement { get; }

        public abstract char ParameterPrefix { get; }

        public abstract char ParameterLeftToken { get; }

        public abstract char ParameterRightToken { get; }

        public abstract char WildCharToken { get; }

        public abstract char WildSingleCharToken { get; }
    }
}
