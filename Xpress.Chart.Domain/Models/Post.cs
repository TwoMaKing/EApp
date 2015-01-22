using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpress.Chat.Domain.Models
{
    public class Post : EntityBase, IAggregateRoot
    {
        private Topic topic;

        private User author;

        private string content = string.Empty;

        private DateTime creationDateTime = DateTime.Now;

        public Post() : base() { }

        public Post(Topic topic, User author, string content)
        {
            this.topic = topic;
            this.author = author;
            this.content = content;
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

        public int PraiseCount { get; private set; }

        #region Domain Business
        
        public void Publish() 
        { 
            // send to Message Queue.
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
