using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Serialization;
using EApp.Core.Application;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.Model;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Redis.Pipeline;

namespace EApp.Common.Cache
{
    public class RedisManager : ICacheManager
    {
        private static PooledRedisClientManager redisClientManager;

        private static RedisClient redisClient;

        private int expire = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.TimeOutSeconds;

        static RedisManager()
        {
            CreateRedisClient();
        }

        private static void CreateRedisClient() 
        {
            string writeServerList = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.WriteHosts;
            string readOnlyServerList = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.ReadOnlyHosts;

            string[] writeHosts = writeServerList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] readOnlyHosts = readOnlyServerList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            RedisClientManagerConfig config = new RedisClientManagerConfig();
            config.MaxWritePoolSize = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.MaxWritePoolSize;
            config.MaxReadPoolSize = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.MaxReadPoolSize;
            config.AutoStart = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.AutoStart;

            redisClientManager = new PooledRedisClientManager(writeHosts, readOnlyHosts, config);

            redisClient = (RedisClient)redisClientManager.GetClient();
        }

        public void AddItem(string key, object item)
        {
            this.AddItem(key, item, this.expire);
        }

        public void AddItem(string key, object item, int expire)
        {
            byte[] objectBytes = SerializationManager.SerializeToBinary(item);

            redisClient.Set(key, objectBytes, new TimeSpan(expire));
        }

        public void AddItem<T>(string key, T item)
        {
            this.AddItem<T>(key, item, this.expire);
        }

        public void AddItem<T>(string key, T item, int expire)
        {
            redisClient.Set<T>(key, item, new TimeSpan(expire));
        }

        public bool ContainsKey(string key)
        {
            return redisClient.ContainsKey(key);
        }

        public object GetItem(string key)
        {
            byte[] objectBytes = redisClient.Get(key);

            return SerializationManager.DeserializeFromBinary(objectBytes);
        }

        public T GetItem<T>(string key)
        {
            return redisClient.Get<T>(key);
        }

        public void RemoveItem(string key)
        {
            redisClient.Remove(key);
        }

        public void FlushAll()
        {
            redisClient.FlushAll();
        }
    }
}
