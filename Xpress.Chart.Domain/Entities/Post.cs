using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;

namespace Xpress.Chart.Domain
{
    public class Post : EntityBase, IAggregateRoot
    {
        private int id;

        private Topic topic;

        private User author;

        private string body = string.Empty;

        private DateTime creationDateTime = DateTime.Now;

        public Post(Topic topic, User author, string body)
        {
            this.topic = topic;
            this.author = author;
            this.body = body;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
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

        public string Body
        {
            get 
            {
                return this.body;
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
    }
}
