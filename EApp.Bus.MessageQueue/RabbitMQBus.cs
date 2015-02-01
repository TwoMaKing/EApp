using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;

namespace EApp.Bus.MessageQueue
{
    public class RabbitMQBus<TMessage> : IMessageQueueBus<TMessage> where TMessage : class
    {
        private ConnectionFactory connectionFactory = new ConnectionFactory();

        private IConnection connection;

        private IModel channel;

        private string queueName = string.Empty;

        private const string queueNamePrefixKey = "MQ.";

        public RabbitMQBus() : this(queueNamePrefixKey + typeof(TMessage).Name) { }

        public RabbitMQBus(string queueName) 
        {
            this.connectionFactory.HostName = "";

            this.connection = this.connectionFactory.CreateConnection();

            this.channel = this.connection.CreateModel();

            this.channel.QueueDeclare(this.queueName, true, false, false, null);
        }

        public bool DistributedTransactionSupported
        {
            get { return true; }
        }

        public void Publish(TMessage message)
        {
            this.channel.BasicPublish(string.Empty, this.queueName, null, null);
            this.Committed = false;
        }

        public void Publish(IEnumerable<TMessage> messages)
        {
            throw new NotImplementedException();
        }

        public bool Committed
        {
            get;
            private set;
        }

        public void Commit()
        {
            this.channel.TxCommit();
            this.Committed = true;
        }

        public void Rollback()
        {
            this.channel.TxRollback();
            this.Committed = false;
        }

        public void Dispose()
        {
            this.channel.Close();
            this.channel.Dispose();
        }
    }
}
