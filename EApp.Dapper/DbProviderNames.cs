using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{

    /// <summary>
    /// Db Provider names
    /// </summary>
    public sealed class DbProviderNames
    {
        private DbProviderNames() { }

        /// <summary>
        /// Oledb Provider
        /// </summary>
        public const string Oledb = "System.Data.OleDb";
        /// <summary>
        /// MySQL Provider
        /// </summary>
        public const string MySQL = "MySql.Data.MySqlClient";
        /// <summary>
        /// Oracle ODP Provider
        /// </summary>
        public const string Oracle_ODP = "Oracle.DataAccess.Client";
        /// <summary>
        /// Microsoft Oracle Provider
        /// </summary>
        public const string Oracle = "System.Data.OracleClient";
        /// <summary>
        /// SqlServer Provider
        /// </summary>
        public const string SqlServer = "System.Data.SqlClient";
        /// <summary>
        /// SQLite Provider
        /// </summary>
        public const string SQLite = "System.Data.SQLite";

    }
}
