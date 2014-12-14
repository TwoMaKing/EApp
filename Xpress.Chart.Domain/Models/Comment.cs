using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;

namespace Xpress.Chart.Domain.Models
{
    public class Comment : EntityBase, IAggregateRoot
    {
        private Post post;

        private User author;

        private string content = string.Empty;

        private DateTime creationDateTime = DateTime.Now;

        public Comment(Post post, User author, string content)
        {
            this.post = post;
            this.author = author;
            this.content = content;
        }

        public Post Post 
        {
            get 
            {
                return this.post;
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

        public static Comment Create(Post post, User author, string content) 
        {
            return new Comment(post, author, content);
        }
    }
}
