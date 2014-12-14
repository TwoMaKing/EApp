using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;

namespace Xpress.Chart.Domain.Models
{
    public class Post : EntityBase, IAggregateRoot
    {
        private Topic topic;

        private User author;

        private string content = string.Empty;

        private DateTime creationDateTime = DateTime.Now;

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
        }

        public User Author 
        {
            get 
            {
                return this.author;
            }
        }

        public string Content
        {
            get 
            {
                return this.content;
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

        #region Domain Business

        public static Post Create(Topic topic, User author, string content) 
        {
            return new Post(topic, author, content);
        }

        #endregion
    }
}
