using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EApp.Core.Application;
using EApp.Core.DomainDriven.Bus;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Redis.Support.Locking;
using ServiceStack.Redis.Support.Queue.Implementation;

namespace EApp.Bus.MessageQueue
{
    public class RedisMQBus<TMessage> : IMessageQueueBus<TMessage> where TMessage : class
    {
        private PooledRedisClientManager redisClientManager;

        private RedisClient redisClient;

        private IRedisTypedClient<TMessage> redistTypedClient;

        private IRedisTypedTransaction<TMessage> redisTypedTransaction;

        private IDistributedLock redisDistributedLock;

        private string queueName = string.Empty;

        private const string queueNamePrefixKey = "MQ.";

        private const string lockName = "MQ.LOCK";

        private long lockExpire;

        public RedisMQBus() : this(queueNamePrefixKey + typeof(TMessage).Name.ToUpper()) { }

        public RedisMQBus(string queueName)
        {
            this.queueName = queueName;

            this.CreateRedisClient();
        }

        private void CreateRedisClient() 
        {
            string writeServerList = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.WriteHosts;
            string readOnlyServerList = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.ReadOnlyHosts;

            string[] writeHosts = writeServerList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] readOnlyHosts = readOnlyServerList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            RedisClientManagerConfig config = new RedisClientManagerConfig();
            config.MaxWritePoolSize = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.MaxWritePoolSize;
            config.MaxReadPoolSize = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.MaxReadPoolSize;
            config.AutoStart = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Redis.AutoStart;

            this.redisClientManager = new PooledRedisClientManager(writeHosts, readOnlyHosts, config);

            this.redisClient = (RedisClient)redisClientManager.GetClient();

            this.redistTypedClient = this.redisClient.As<TMessage>();

            this.redisDistributedLock = new DistributedLock();

            this.redisDistributedLock.Lock(lockName, 0, 0, out this.lockExpire, this.redisClient);

            this.redisTypedTransaction = this.redistTypedClient.CreateTransaction();
        }

        public void Publish(TMessage message)
        {
            this.redisTypedTransaction.QueueCommand((r) => r.EnqueueItemOnList(r.Lists[this.queueName], message));

            this.Committed = false;
        }

        public void Publish(IEnumerable<TMessage> messages)
        {
            if (messages != null &&
                messages.Count() > 0)
            {
                foreach (TMessage message in messages)
                {
                    this.redisTypedTransaction.QueueCommand((r) => r.EnqueueItemOnList(r.Lists[this.queueName], message));
                }
            }
            
            this.Committed = false;
        }

        public bool Committed
        {
            get;
            private set;
        }

        public void Commit()
        {
            this.Committed = this.redisTypedTransaction.Commit();

            this.redisDistributedLock.Unlock(lockName, this.lockExpire, this.redisClient);
        }

        public void Rollback()
        {
            this.redisTypedTransaction.Rollback();
            this.Committed = false;
        }

        public void Dispose()
        {
            this.redisTypedTransaction.Dispose();
            this.redisClient.Dispose();
            this.redisClientManager.Dispose();
        }

    }
}
