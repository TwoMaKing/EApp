using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.DomainDriven.Infrastructure.Domain;

namespace EApp.Domain.Entities
{
    public class Post : EntityBase, IAggregateRoot
    {
        private int id;

        private Topic topic;

        private User author;

        private string body = string.Empty;

        private DateTime creationDateTime = DateTime.Now;

        public Post(int id, Topic topic, User author, string body)
        {
            this.topic = topic;
            this.author = author;
            this.body = body;
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


        public object Id
        {
            get
            {
                return this.id;
            }
        }
    }
}
