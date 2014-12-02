using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;


namespace EApp.Common.DataAccess
{

    /// <summary>
    /// The db provider factory.
    /// </summary>
    /// <remarks></remarks>
    public sealed class DbProviderFactory
    {

        #region "private Memeber"

        private static Dictionary<string, DbProvider> providerCache = new Dictionary<string, DbProvider>();

        private DbProviderFactory()
        {
        }

        #endregion

        /// <summary>
        /// Creates the db provider.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="connectionString">The conn STR.</param>
        /// <returns>The db provider.</returns>
        public static DbProvider CreateDbProvider(string assemblyName, string className, string connectionString)
        {
            string cacheKey = string.Concat(assemblyName, className, connectionString);

            if (providerCache.ContainsKey(cacheKey))
            {
                return providerCache[cacheKey];

            }
            else
            {
                Assembly ass = null;
                if (string.IsNullOrEmpty(assemblyName))
                {
                    ass = typeof(DbProvider).Assembly;
                }
                else
                {
                    ass = Assembly.Load(assemblyName);
                }

                DbProvider retProvider = (DbProvider)ass.CreateInstance(className, true, BindingFlags.Default, null, new object[] { connectionString }, null, null);

                providerCache.Add(cacheKey, retProvider);

                return retProvider;
            }

        }

        public static DbProvider CreateDbProvider(string connectionStringName)
        {
            ConnectionStringSettings connStrSetting = ConfigurationManager.ConnectionStrings[connectionStringName];

            string connectionString = connStrSetting.ConnectionString;
            string providerName = connStrSetting.ProviderName;
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
        }

    }

}
