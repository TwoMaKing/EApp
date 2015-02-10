using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Bus.MessageQueue;
using EApp.Domain.Core;

namespace Xpress.Chat.Domain.Models
{
    public class Post : AggregateRoot
    {
        private Topic topic;

        private User author;

        private string content = string.Empty;

        private DateTime creationDateTime = DateTime.Now;

        private int praiseCount;

        public Post() : base() { }

        public Post(Topic topic, User author, string content)
        {
            this.topic = topic;
            this.author = author;
            this.content = content;
        }

        public int TopicId { get; set; }

        public int AuthorId { get; set; }

        public string Content
        {
            get 
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public DateTime CreationDateTime
        {
            get 
            {
                return this.creationDateTime;
            }
            set
            {
                this.creationDateTime = value;
            }
        }

        public int PraiseCount 
        { 
            get 
            { 
                return praiseCount; 
            }
            private set
            {
                this.praiseCount = value;
            }
        }

        public Topic Topic
        {
            get
            {
                return this.topic;
            }
            set
            {
                this.topic = value;
            }
        }

        public User Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }

        #region Domain Business
        
        public void Publish() 
        { 
            //send to Message Queue.

            using (RedisMQBus<Post> postMQ = new RedisMQBus<Post>("MQ_Post"))
            {
                postMQ.Publish(this);

                postMQ.Commit();
            }
        }

        public void Forward() 
        { 
            
        }

        public void Praise()
        {

        }

        public void Collect() 
        { 
            
        }

        public static Post Create(Topic topic, User author, string content) 
        {
            return new Post(topic, author, content);
        }

        #endregion
    }

}
