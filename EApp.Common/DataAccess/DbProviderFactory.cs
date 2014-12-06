using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using EApp.Core.Exceptions;


namespace EApp.Common.DataAccess
{

    /// <summary>
    /// The db provider factory.
    /// </summary>
    /// <remarks></remarks>
    public sealed class DbProviderFactory
    {

        #region "private Memeber"

        private static DbProvider defaultDbProvider;

        private static Dictionary<string, DbProvider> providerCache = new Dictionary<string, DbProvider>();

        private DbProviderFactory() { }

        #endregion

        /// <summary>
        /// Creates the db provider.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="classTypeName">Name of the class.</param>
        /// <param name="connectionString">The conn STR.</param>
        /// <returns>The db provider.</returns>
        public static DbProvider CreateDbProvider(string assemblyName, string classTypeName, string connectionString)
        {
            string cacheKey = string.Concat(assemblyName, classTypeName, connectionString);

            if (providerCache.ContainsKey(cacheKey))
            {
                return providerCache[cacheKey];
            }
            else
            {
                Assembly assembly = null;

                if (string.IsNullOrEmpty(assemblyName))
                {
                    assembly = typeof(DbProvider).Assembly;
                }
                else
                {
                    assembly = Assembly.Load(assemblyName);
                }

                DbProvider dbProvider = (DbProvider)assembly.CreateInstance(
                    classTypeName, true, BindingFlags.Default, null, new object[] { connectionString }, null, null);

                providerCache.Add(cacheKey, dbProvider);

                return dbProvider;
            }
        }

        /// <summary>
        /// Create the specified DbProvider instance by the name of connection string.
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static DbProvider CreateDbProvider(string connectionStringName)
        {
            ConnectionStringSettings connStrSetting = ConfigurationManager.ConnectionStrings[connectionStringName];

            string connectionString = connStrSetting.ConnectionString;
            
            string providerName = connStrSetting.ProviderName;

            string[] assAndClass = providerName.Split(new char[] { ',' });

            try
            {
                if (assAndClass.Length.Equals(2))
                {
                    return CreateDbProvider(assAndClass[0].Trim(), assAndClass[1].Trim(), connectionString);
                }
                else
                {
                    return CreateDbProvider(string.Empty, providerName, connectionString);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static DbProvider CreateDefaultDbProvider() 
        {
            if (ConfigurationManager.ConnectionStrings == null ||
                ConfigurationManager.ConnectionStrings.Count.Equals(0))
            {
                throw new ConfigException("Please provide a connection string including name, provider and connection string.");
            }

            ConnectionStringSettings connStrSetting = ConfigurationManager.ConnectionStrings[0];

            string connectionString = connStrSetting.ConnectionString;

            string providerName = connStrSetting.ProviderName.Trim();

            string[] assAndClass = providerName.Split(new char[] { ',' });

            try
            {
                if (assAndClass.Length == 1)
                {
                    return CreateDbProvider(assAndClass[0].Trim(), assAndClass[1].Trim(), connectionString);
                }
                else
                {
                    return CreateDbProvider(string.Empty, providerName, connectionString);
                }

            }
            catch (Exception ex)
            {  

                return null;
            }
        }

        public static DbProvider Default
        {
            get
            {
                if (defaultDbProvider == null)
                {
                    defaultDbProvider = CreateDefaultDbProvider();
                }

                return defaultDbProvider;
            }
        }

    }

}
