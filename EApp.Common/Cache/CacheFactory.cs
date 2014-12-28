using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.Exceptions;

namespace EApp.Common.Cache
{
    public sealed class CacheFactory
    {
        private static Dictionary<string, ICacheManager> cacheManagerDictionary = new Dictionary<string, ICacheManager>();

        private static object lockObject = new object();

        private CacheFactory() { }

        public static ICacheManager GetCacheManager() 
        {
            return GetCacheManager(EAppRuntime.Instance.CurrentApp.ConfigSource.Config.CacheManagers.Default);
        }

        public static ICacheManager GetCacheManager(string name)
        {
            string cacheManagerTypeName = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.CacheManagers[name].Type;

            if (cacheManagerDictionary.ContainsKey(cacheManagerTypeName))
            {
                return (ICacheManager)cacheManagerDictionary[cacheManagerTypeName];
            }

            if (string.IsNullOrEmpty(cacheManagerTypeName))
            {
                throw new ConfigException("The cache manager has not been defined in the ConfigSource.");
            }

            Type cacheManagerType = Type.GetType(cacheManagerTypeName);

            if (cacheManagerType == null)
            {
                throw new InfrastructureException("The Cache Manager defined by the type {0} doesn't exist.", cacheManagerTypeName);
            }

            if (!typeof(ICacheManager).IsAssignableFrom(cacheManagerType))
            {
                throw new ConfigException("Type '{0}' is not a Cache Manager.", cacheManagerType);
            }

            ICacheManager cacheManager;

            lock (lockObject)
            {
                if (!cacheManagerDictionary.ContainsKey(cacheManagerTypeName))
                {
                    cacheManager = (ICacheManager)EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve(cacheManagerType,
                                                                                                          cacheManagerTypeName);

                    cacheManagerDictionary.Add(cacheManagerTypeName, cacheManager);
                }
                else
                {
                    cacheManager = (ICacheManager)cacheManagerDictionary[cacheManagerTypeName];
                }
            }

            return cacheManager;
        }

    }
}
