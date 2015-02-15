using EApp.Dapper.Mapping;
using EApp.Dapper.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public class DbConfiguration : IDisposable
    {
        private readonly static HashSet<string> providerNames = new HashSet<string>();
        
        private readonly static Dictionary<string, IDbDriver> currentDbDrivers = new Dictionary<string, IDbDriver>();

        private string providerName;

        private string connectionString;

        private DbProviderFactory dbProviderFactory;

        private IDbDriver dbDriver;

        private DbConfiguration(string providerName, string connectionString, DbProviderFactory dbProviderFactory)
        {
            this.providerName = providerName;
            this.connectionString = connectionString;
            this.dbProviderFactory = dbProviderFactory;
            this.dbDriver = currentDbDrivers[providerName];
        }

        static DbConfiguration()
        {
            currentDbDrivers.Add(DbProviderNames.SqlServer, new SqlServerDbDriver());

            DataTable dbProviderFactories = DbProviderFactories.GetFactoryClasses();

            if (dbProviderFactories.Rows != null &&
                dbProviderFactories.Rows.Count > 0)
            {
                foreach (DataRow row in dbProviderFactories.Rows)
                {
                    string providerName = row["InvariantName"].ToString();

                    providerNames.Add(providerName);
                }
            }
        }

        public static DbConfiguration Default 
        {
            get
            {
                return null;
            }
        }

        public static DbConfiguration Configure()
        {
            return Configure(ConfigurationManager.ConnectionStrings[0].Name);
        }

        public static DbConfiguration Configure(string connectionStringSectionName)
        {
            if (string.IsNullOrEmpty(connectionStringSectionName) ||
                string.IsNullOrWhiteSpace(connectionStringSectionName))
            {
                throw new ArgumentNullException("Connection string section name cannot be null or empty");
            }

            string providerName = ConfigurationManager.ConnectionStrings[connectionStringSectionName].ProviderName;

            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringSectionName].ConnectionString;

            return Configure(providerName, connectionString);
        }

        public static DbConfiguration Configure(string providerName, string connectionString)
        {
            if (string.IsNullOrEmpty(providerName) ||
                string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException("Provider Name of database cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(connectionString) ||
                string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("Connection string of database cannot be null or empty.");
            }

            try
            {
                var dbProviderFactory = DbProviderFactories.GetFactory(providerName);

                return new DbConfiguration(providerName, connectionString, dbProviderFactory);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DbConfiguration Configure(DbConnection connection)
        {
            

            return null;
        }

        public string DbProviderName 
        { 
            get
            {
                return this.providerName;
            }
        }

        public string ConnectionString
        {
            get 
            {
                return this.connectionString;
            }
        }

        public DbProviderFactory DbProviderFactory
        {
            get
            {
                return this.dbProviderFactory;
            }
        }

        public IDbDriver DbDriver
        {
            get
            {
                return this.dbDriver;
            }
        }

        public IDbContext DbContext
        {
            get
            {
                return null;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        #region Protected methods

        protected IDbContext CreateDbContext()
        {
            return null;
        }

        protected virtual void Dispose(bool disposing) 
        { 
            if (disposing)
            {

            }
        }

        #endregion

        #region Private methods

        private IEntityMapping CreateEntityMapping(Type entityType)
        {
            return null;
        }

        #endregion
    }
}
